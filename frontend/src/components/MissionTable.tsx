import { useMemo, useState } from 'react';
import {
  MantineReactTable,
  useMantineReactTable,
  type MRT_ColumnDef,
} from 'mantine-react-table';
import { useQuery, useQueryClient } from '@tanstack/react-query';
import {
  createMission,
  deleteMission,
  getProjectMissions,
  getMyMissions,
  updateMission,
} from '../services/Api';
import { decodeToken } from '../services/TokenDecode';
import toast from 'react-hot-toast';
import { useNavigate } from 'react-router-dom';
import { Button, Modal, Text } from '@mantine/core';
import MissionPopUp from './PopUp';
import MyTimeline from './Timeline';
import { TiEdit } from 'react-icons/ti';
import { MdDelete } from 'react-icons/md';
import {
  MissionCreate,
  MissionUpdate,
  MissionUpdateForhistory,
  DeleteMissionSuccessResponse,
  DeleteMissionFailResponse,
} from '../types/mission-update';
import { MissionGetResponse } from '../types/mission-get-response';
import { MyMissionResponse } from '../types/mymission-response';
 
interface Props {
  projectId?: number;
  today?: boolean;
}

const ProjectMissionsTable: React.FC<Props> = ({ projectId,today=false }) => {
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const [opened, setOpened] = useState(false);
  const [newProj, setNewProj] = useState(false);
  const [selectedMissionId, setSelectedMissionId] = useState<number | null>(null);
  const [selectedMission, setSelectedMission] = useState<any>(null);
  const [deleteConfirm, setDeleteConfirm] = useState<{ open: boolean; id: number | null }>({ open: false, id: null });
  const [oldData, setOldData] = useState<MissionUpdateForhistory>();
  const [myproj, setmyproj] = useState<boolean>(false);

  const token = decodeToken();
  const checkAdmin = token?.isAdmin === 'True';
  const myid = token?.userid;

  const {
    data: missionResponse,
    isLoading: isLoadingMissions,
    isError: isErrorMissions,
    isFetching: isFetchingMissions,
  } = useQuery<MissionGetResponse>({
    queryKey: ['missions', projectId],
    queryFn: () => getProjectMissions(projectId!),
    enabled: !!projectId,
  });

  const {
    data: myMissionsResponse,
    isLoading: isLoadingMyMissions,
    isError: isErrorMyMissions,
    isFetching: isFetchingMyMissions,
  } = useQuery<MyMissionResponse>({
    queryKey: ['my-missions',today],
    queryFn: () => getMyMissions(today),
    enabled: !projectId,
  });

  const missions = useMemo(() => {
    if (projectId) {
      return missionResponse?.data ?? [];
    } else {
      return (
        myMissionsResponse?.data.map((m) => ({
          id: m.id,
          title: m.name,
          description: m.description,
          createdAt: m.myMissionProject?.createdat,
          status: m.status,
          customerName: m.myMissionProject?.customer?.name ?? '',
          projectId: m.myMissionProject?.id ?? 0,
        })) ?? []
      );
    }
  }, [projectId, missionResponse, myMissionsResponse]);

  const canISee = useMemo(() => {
    if (!projectId) return true;
    return checkAdmin || selectedMission?.assignedUsers?.includes(myid);
  }, [checkAdmin, selectedMission, myid, projectId]);

  const handleView = (id: number) => {
    const mission = (missions as any[]).find((mission: any) => mission.id === id);
    setSelectedMissionId(id);
    setSelectedMission(mission);
    setOldData({
      title: mission?.title,
      description: mission?.description,
      status: mission?.status?.status,
    });
    setOpened(true);
    if(projectId == undefined)
      setmyproj(true);
  };

  const handleConfirmDelete = async () => {
    const id = deleteConfirm.id;
    if (!id) return;
    const toastId = toast.loading('Görev siliniyor...');
    try {
      const res = await deleteMission(id);
      if ('success' in res && res.success === true) {
        toast.success(`Görev silindi: ${id}`);
        await queryClient.invalidateQueries({ queryKey: ['missions', projectId] });
        await queryClient.invalidateQueries({ queryKey: ['my-missions'] });
      } else {
        toast.error('Silme işlemi başarısız.');
      }
    } catch (err) {
      toast.error('Beklenmeyen bir hata oluştu.');
      console.error(err);
    } finally {
      toast.dismiss(toastId);
      setDeleteConfirm({ open: false, id: null });
    }
  };

  const handleDelete = (id: number) => {
    setDeleteConfirm({ open: true, id });
  };

  const handleMissionChange = (field: string, value: any) => {
    setSelectedMission({ ...selectedMission, [field]: value });
  };

  const handleMissionUpdate = async () => {
    const updateBody: MissionUpdate = {
      id: selectedMission.id,
      title: selectedMission.title,
      description: selectedMission.description,
      status: Number(selectedMission.status?.status),
      projectId: projectId ?? selectedMission?.projectId ?? 0,
      assignedUserId: Array.isArray(selectedMission.assignedUsers)
        ? selectedMission.assignedUsers[0]
        : selectedMission.assignedUser?.userId ?? 0,
    };
  
    if (updateBody.id !== 0) {
      await updateMission(updateBody, oldData);
      toast.success('Görev güncellendi.');
    } else {
      const createBody: MissionCreate = {
        title: updateBody.title,
        description: updateBody.description,
        status: updateBody.status,
        projectId: updateBody.projectId,
        assignedUserId: updateBody.assignedUserId,
      };
      await createMission(createBody);
      toast.success('Yeni görev oluşturuldu.');
    }

    await queryClient.invalidateQueries({ queryKey: ['missions', projectId] });
    await queryClient.invalidateQueries({ queryKey: ['my-missions'] });
    setOpened(false);
    setSelectedMission(null);
  };

  const columns = useMemo<MRT_ColumnDef<any>[]>(
    () => {
      const baseColumns: MRT_ColumnDef<any>[] = [
        { accessorKey: 'id', header: 'Görev NO', size: 60 },
        { accessorKey: 'title', header: 'Görev Başlığı' },
        { accessorKey: 'description', header: 'Açıklama' },
        {
          accessorFn: (row) => new Date(row.createdAt).toLocaleString(),
          id: 'createdAt',
          header: 'Oluşturulma Tarihi',
        },
      ];

      if (projectId === undefined) {
        baseColumns.push({ accessorKey: 'customerName', header: 'Müşteri' });
        baseColumns.push({ accessorKey: 'projectId', header: 'Proje No' });
        baseColumns.push({ accessorKey: 'status.statusName', header: 'Durum' });
      } else {
        baseColumns.push({ accessorKey: 'status.statusName', header: 'Durum' });
        baseColumns.push({ accessorKey: 'assignedUser.username', header: 'Atanan Kullanıcı' });
      }

      return baseColumns;
    },
    [projectId]
  );

  const table = useMantineReactTable({
    columns,
    data: missions,
    enableColumnOrdering: true,
    enableRowActions: true,
    positionActionsColumn: 'last',
    renderRowActions: ({ row }) => (
      <div style={{ display: 'flex', gap: '8px' }}>
        <Button size="xs" variant="light" color="blue" onClick={() => handleView(row.original.id)}>
          <TiEdit />
        </Button>
        {canISee && (
          <Button size="xs" variant="light" color="red" onClick={() => handleDelete(row.original.id)}>
            <MdDelete />
          </Button>
        )}
      </div>
    ),
    state: {
      isLoading: isLoadingMissions || isLoadingMyMissions,
      showAlertBanner: isErrorMissions || isErrorMyMissions,
      showProgressBars: isFetchingMissions || isFetchingMyMissions,
    },
    mantineTableContainerProps: {
      sx: { minHeight: '300px' },
    },
    mantineToolbarAlertBannerProps:
      isErrorMissions || isErrorMyMissions
        ? { color: 'red', children: 'Görevler yüklenirken hata oluştu.' }
        : undefined,
  });

  return (
    <>
      {projectId && (
        <Button
          variant="filled"
          color="green"
          onClick={() => {
            setSelectedMission({
              id: 0,
              title: '',
              description: '',
              status: { status: 0, statusName: 'Bekliyor' },
              assignedUsers: [],
              projectId: projectId ?? 0,
            });
            setOpened(true);
            setNewProj(true);
          }}
          style={{ marginBottom: 12 }}
        >
          + Yeni Görev Ekle
        </Button>
      )}

      <MantineReactTable table={table} />

      {checkAdmin && projectId && (
        <div className="p-10 m-10 overflow-auto">
          <MyTimeline projectId={projectId} />
        </div>
      )}

      <MissionPopUp
        opened={opened}
        onClose={() => setOpened(false)}
        mission={selectedMission}
        onChange={handleMissionChange}
        onSave={handleMissionUpdate}
        isNew={newProj}
        mymission={myproj}
      />

      <Modal
        opened={deleteConfirm.open}
        onClose={() => setDeleteConfirm({ open: false, id: null })}
        title="Görev Sil"
        centered
      >
        <Text>Bu görevi silmek istediğinize emin misiniz?</Text>
        <div style={{ display: 'flex', justifyContent: 'flex-end', marginTop: 16, gap: 8 }}>
          <Button variant="default" onClick={() => setDeleteConfirm({ open: false, id: null })}>
            İptal
          </Button>
          <Button color="red" onClick={handleConfirmDelete}>
            Sil
          </Button>
        </div>
      </Modal>
    </>
  );
};

export default ProjectMissionsTable;
