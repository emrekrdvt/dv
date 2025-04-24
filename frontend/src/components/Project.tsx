import React, { useMemo, useEffect, useState } from 'react';
import {
    MantineReactTable,
    useMantineReactTable,
    type MRT_ColumnDef,
} from 'mantine-react-table';
import { getProject } from '../services/Api';
import { ProjectResponse } from '../types/project';
import { decodeToken } from '../services/TokenDecode';
import { useNavigate } from 'react-router-dom';


const MyTable: React.FC = () => {
    const [data, setData] = useState<ProjectResponse[]>([]);
    const [owner, setOwner] = useState<boolean>();
    const token = decodeToken();
    const isAdmin = Boolean(token?.isAdmin);
    const navigate = useNavigate();
    

    useEffect(() => {
        const fetchData = async () => {
            try {
                const projects: ProjectResponse[] = await getProject();
                console.log(projects)
                var caniEditMission = projects.some((project) => {
                    return project.projectUsers.some((user) => user.userId === token?.userid);
                });
                setOwner(caniEditMission);
                setData(projects);
            } catch (error) {
                console.error('Veriler alınırken hata oluştu:', error);
            }
        };

        fetchData();
    }, []);

    const columns = useMemo<MRT_ColumnDef<ProjectResponse>[]>(
        () => [
            {
                accessorKey: 'projectId',
                header: 'Proje Numarası',
            },
            {
                accessorKey: 'name',
                header: 'Proje Adı',
            },
            {
                accessorKey: 'description',
                header: 'Açıklama',
            },
            {
                accessorKey: 'customer.name',
                header: 'Müşteri Adı',
            },
            {
                accessorKey: 'missions',
                header: 'Görev Sayısı',
                Cell: ({ cell }) => cell.getValue<ProjectResponse['missions']>().length,
            },
            {
                accessorKey: 'projectUsers',
                header: 'Projeye Atanmış Kullanıcı Sayısı',
                Cell: ({ cell }) => cell.getValue<ProjectResponse['projectUsers']>().length,
            },
        ],
        []
    );

    const table = useMantineReactTable({
        columns,
        data,
        enableColumnActions: true,
        enableColumnOrdering: true,
        enablePinning: true,
        enableRowActions: true,
        positionActionsColumn: 'last',
        renderRowActions: ({ row }) => (
            <div style={{ display: 'flex', gap: '8px' }}>
                <button
                    onClick={() => navigate(`/project/${row.original.projectId}`)}
                    style={{
                        padding: '4px 8px',
                        backgroundColor: '#007BFF',
                        color: '#fff',
                        border: 'none',
                        borderRadius: '4px',
                        cursor: 'pointer',
                    }}
                >
                    Görüntüle
                </button>
                {(!isAdmin) && (
                    <button
                        onClick={() => alert(`Proje Silindi: ${row.original.projectId}`)}
                        style={{
                            padding: '4px 8px',
                            backgroundColor: '#FF0000',
                            color: '#fff',
                            border: 'none',
                            borderRadius: '4px',
                            cursor: 'pointer',
                        }}
                    >
                        Sil
                    </button>
                )}
            </div>
        ),
        getRowId: (row) => row.projectId.toString(),


        displayColumnDefOptions: {
            'mrt-row-actions': {
                header: '',
                size: 120,

            }
        }
    });

    return <MantineReactTable table={table} />;
};

export default MyTable;