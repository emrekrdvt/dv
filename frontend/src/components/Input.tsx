import React, { useEffect, useState } from "react";
import { CustomerResponse } from "../types/customer-response";
import { getCustomer } from "../services/Api";

interface InputProps extends React.InputHTMLAttributes<HTMLInputElement | HTMLSelectElement> {
  label: string;
  flag1?: boolean;
  flag2?: boolean;
  value?: string | number;
  onChange?: (e: React.ChangeEvent<any>) => void;
}

export const Input: React.FC<InputProps> = ({ label, flag1, ...props }) => {
  const [customer, setCustomer] = useState<CustomerResponse>();

  useEffect(() => {
    const fetchCustomer = async () => {
      try {
        const customers: CustomerResponse = await getCustomer();
        setCustomer(customers);
      } catch (error) {
        console.error("Müşteriler alınırken hata oluştu:", error);
      }
    };

    fetchCustomer();
  }, []);

  return (
    <div className="mb-4">
      <label className="block text-sm font-medium text-gray-700 mb-1">{label}</label>
      {flag1 ? (
        <select
          {...props}
          className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5"
        >
          <option value="">Müşteri Seçiniz</option>
          {customer?.data.map((item) => (
            <option key={item.id} value={item.id}>
              {item.name}
            </option>
          ))}
        </select>
      ) : (
        <input
          {...props}
          className="w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
        />
      )}
    </div>
  );
};
