import { useEffect, useState } from 'react';
import { Button, Modal, TextInput, Group, Text, Select } from '@mantine/core';
import { createCustomer, createUser, getCustomer, getUsers } from '../services/Api';
import { CreateUserModel } from '../types/CreateUser';
import toast from 'react-hot-toast';
import { CustomerDto, CustomerResponse } from '../types/customer-response';

interface UserDto {
    id: number;
    username: string;
    email: string;
    isAdmin: boolean;
}

const AdminPage: React.FC = () => {
    const [userModalOpened, setUserModalOpened] = useState(false);
    const [usersModalOpened, setUsersModalOpened] = useState(false);
    const [username, setUsername] = useState('');
    
    const [customerOpened, setCustomerOpened] = useState(false);
    const [companyName, setCompanyName] = useState('');
    const [comapnies, setCompanys] = useState<CustomerDto[]>([]);
    const [customersOpened, setCustomersOpened] = useState(false);

    const [email, setEmail] = useState('');
    const [isAdmin, setIsAdmin] = useState('false');
    const [users, setUsers] = useState<UserDto[]>([]);

    const handleCreateUser = async () => {
        const userModel: CreateUserModel = {
            email,
            username,
            admin: isAdmin === 'true',
        };

        const response = await createUser(userModel, false);
        toast.success('Kullanıcı başarıyla oluşturuldu!');
        setUserModalOpened(false);
        setUsername('');
        setEmail('');
        setIsAdmin('false');
    };
    const handleCreatCompany = async () => {
        const companyModel: any = {
            name: companyName,
        };
        const response = await createCustomer(companyModel);
        toast.success('Firma başarıyla oluşturuldu!');
        setCustomerOpened(false);
        setCompanyName('');
    };
    const handleFetchUsers = async () => {
        try {
            const response = await getUsers();
            if (response.success) {
                setUsers(response.data);
                setUsersModalOpened(true);
            } else {
                toast.error(response.message || 'Kullanıcılar alınamadı');
            }
        } catch (error) {
            toast.error('Kullanıcılar alınırken hata oluştu');
        }
    };

    const handleUpdateUsers = async () => {
        try {

            for (const user of users) {
                const userModel: CreateUserModel = {
                    id: user.id,
                    email: user.email,
                    username: user.username,
                    admin: user.isAdmin,
                };
                await createUser(userModel, true)
            }
            toast.success('Kullanıcılar başarıyla güncellendi');
            setUsersModalOpened(false);
        } catch (error) {
            toast.error('Güncelleme sırasında hata oluştu');
        }
    };

    const handleFetchcustomer = async () => {
        try {
            const response = await getCustomer();
            if (response.success) {
                setCompanys(response.data);
                setCustomersOpened(true);
            } else {
                toast.error(response.message || 'Firmalar alınamadı');
            }
        } catch (error) {
            toast.error('Firmalar alınırken hata oluştu');
        }
    };


    const deleteUser = async (userid: number) => {
        try {
            const userModel: CreateUserModel = {
                id: userid,
                email: "",
                username: "",
                admin: false,
                isDelMethod: true,
            };
            await createUser(userModel, true)

            toast.success('Başarı ile silindi');
            setUsersModalOpened(false);
        } catch (error) {
            toast.error('Güncelleme sırasında hata oluştu');
        }
    };

    const updateUserField = (id: number, field: keyof UserDto, value: string | boolean) => {
        setUsers((prevUsers) =>
            prevUsers.map((user) =>
                user.id === id ? { ...user, [field]: field === 'isAdmin' ? value === 'true' : value } : user
            )
        );
    };

    return (
        <div className="p-6 max-w-md mx-auto flex flex-col gap-4">
            

            <Button onClick={() => setUserModalOpened(true)} color="blue">
                Kullanıcı Oluştur
            </Button>

            <Button onClick={handleFetchUsers} color="cyan">
                Tüm Kullanıcılar
            </Button>

            <Button onClick={() => setCustomerOpened(true)} color="lime">
                Firma Ekle
            </Button>

            <Button onClick={handleFetchcustomer}  color="teal">
                Tum firmalar
            </Button>

            {/* Kullanıcı Oluştur Modalı */}
            <Modal
                opened={userModalOpened}
                onClose={() => setUserModalOpened(false)}
                title="Yeni Kullanıcı Oluştur"
                centered
            >
                <div className="flex flex-col gap-4">
                    <TextInput
                        label="Kullanıcı Adı"
                        placeholder="username"
                        value={username}
                        onChange={(e) => setUsername(e.currentTarget.value)}
                    />
                    <TextInput
                        label="E-posta"
                        placeholder="email@example.com"
                        value={email}
                        onChange={(e) => setEmail(e.currentTarget.value)}
                    />
                    <Select
                        label="Admin?"
                        placeholder="Seçiniz"
                        value={isAdmin}
                        onChange={(value) => setIsAdmin(value || 'false')}
                        data={[
                            { value: 'true', label: 'Evet' },
                            { value: 'false', label: 'Hayır' },
                        ]}
                    />
                    <Group position="right">
                        <Button onClick={handleCreateUser}>Oluştur</Button>
                    </Group>
                </div>
            </Modal>

            <Modal
                opened={usersModalOpened}
                onClose={() => setUsersModalOpened(false)}
                title="Tüm Kullanıcılar"
                centered
            >
                <div className="flex flex-col space-y-4 max-h-96 overflow-y-auto">
                    {users.map((user) => (
                        <div key={user.id} className="border-b py-2 flex flex-col space-y-2">
                            <TextInput
                                label="Kullanıcı Adı"
                                value={user.username}
                                onChange={(e) => updateUserField(user.id, 'username', e.currentTarget.value)}
                            />
                            <TextInput
                                label="E-posta"
                                value={user.email}
                                onChange={(e) => updateUserField(user.id, 'email', e.currentTarget.value)}
                            />
                            <Select
                                label="Admin?"
                                value={user.isAdmin ? 'true' : 'false'}
                                onChange={(value) => updateUserField(user.id, 'isAdmin', value || 'false')}
                                data={[
                                    { value: 'true', label: 'Evet' },
                                    { value: 'false', label: 'Hayır' },
                                ]}
                            />
                          <div className='flex space-x-2'>
                                <Button onClick={handleUpdateUsers}> Kaydet </Button>
                                <Button onClick={() => deleteUser(user.id)}>Sil</Button>
                                </div>
                        </div>
                    ))}
                    <Group position="right" className="pt-4">
                        <Button onClick={handleUpdateUsers} color="green">
                            Değişiklikleri Kaydet
                        </Button>
                    </Group>
                </div>
            </Modal>


            <Modal
                opened={customerOpened}
                onClose={() => setCustomerOpened(false)}
                title="Yeni Musteri Oluştur"
                centered
            >
                <div className="flex flex-col gap-4">
                    <TextInput
                        label="Firma Adi"
                        placeholder="Firma Adi"
                        value={companyName}
                        onChange={(e) => setCompanyName(e.currentTarget.value)}
                    />
                    <Group position="right">
                        <Button onClick={handleCreatCompany}>Oluştur</Button>
                    </Group>
                </div>
            </Modal>
        
        
             <Modal
                opened={customersOpened}
                onClose={() => setCustomersOpened(false)}
                title="Tüm firmalar"
                centered
            >
                <div className="flex flex-col gap-4 max-h-96 overflow-y-auto">
                    {comapnies.map((cmp) => (
                        <div key={cmp.id} className="border-b py-2 flex flex-col gap-2">
                            <TextInput
                                label="Firma Adi"
                                value={cmp.name}
                                onChange={(e) => updateUserField(cmp.id, 'username', e.currentTarget.value)}
                            />
                            
                            <div className='flex space-x-2'>
                                <Button > Kaydet </Button>
                                <Button >Sil</Button>
                                </div>
                        </div>
                    ))}
                    <Group position="right" className="pt-4">
                        <Button onClick={handleUpdateUsers} color="green">
                            Değişiklikleri Kaydet
                        </Button>
                    </Group>
                </div>
            </Modal> 

        </div>
    );
};

export default AdminPage;
