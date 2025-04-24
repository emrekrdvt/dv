import React from 'react';
import { Modal, TextInput, Select, Group, Button } from '@mantine/core';
import { MultiSelectDropdown } from './MultiSelect';
import { decodeToken } from '../services/TokenDecode';

interface MissionModalProps {
    opened: boolean;
    onClose: () => void;
    mission: any;
    isNew?: boolean;
    mymission?: boolean;
    onChange: (field: string, value: any) => void;
    onSave: () => void;
}



const MissionModal: React.FC<MissionModalProps> = ({ opened, onClose, mission, onChange, onSave, isNew = false,mymission=false }) => {
    if (!mission) return null;

    const assignedUserIds = Array.isArray(mission.assignedUsers)
        ? mission.assignedUsers
        : mission.assignedUser?.userId
            ? [mission.assignedUser.userId]
            : [];

    const token = decodeToken();
    const checkAdmin = token?.isAdmin === "True";
    const myid = Number(token?.userid);
    const isAssigned = assignedUserIds.includes(myid);

    var canI = (checkAdmin || isAssigned || isNew) ? true : false;


    return (
        <Modal opened={opened} onClose={onClose} title={isNew ? "Görev Oluştur" : "Görev Detayları"} size="lg">
            <form>
                <TextInput
                    disabled={!checkAdmin && !isAssigned}
                    label="Görev Başlığı"
                    value={mission.title}
                    onChange={(e) => onChange('title', e.currentTarget.value)}
                    mb="sm"
                />
                <TextInput
                    disabled={!checkAdmin && !isAssigned}

                    label="Açıklama"
                    value={mission.description}
                    onChange={(e) => onChange('description', e.currentTarget.value)}
                    mb="sm"
                />
                <Select
                    disabled={!checkAdmin && !isAssigned}

                    label="Durum"
                    data={[
                        { value: '0', label: 'Bekliyor' },
                        { value: '1', label: 'Devam Ediyor' },
                        { value: '2', label: 'Tamamlandı' },
                    ]}
                    value={mission.status.status.toString()}
                    onChange={(value) => onChange('status', { ...mission.status, status: Number(value) })}
                    mb="sm"
                />

{
    !mymission && (
        <MultiSelectDropdown
        disabled={!checkAdmin && !isAssigned}
        selected={assignedUserIds}
        onChange={(ids) => onChange('assignedUsers', ids)}
    />
    )
}
                
                <Group position="right" mt="md">
                    {canI && (<Button onClick={onSave}>Kaydet</Button>)}
                </Group>
            </form>
        </Modal>
    );
};

export default MissionModal;
