import React, { useEffect, useState } from "react";
import { UserResponse } from "../types/userresponse";
import { getUsers } from "../services/Api";

interface Props {
  selected: number[]; // sadece id'leri içerir
  onChange: (selected: number[]) => void;
  disabled?: boolean
}

export const MultiSelectDropdown: React.FC<Props> = ({ selected, onChange,disabled }) => {
  const [isOpen, setIsOpen] = useState(false);
  const [user, setUser] = useState<UserResponse>();

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const users: UserResponse = await getUsers();
        setUser(users);
      } catch (error) {
        console.error("Veriler alınırken hata oluştu:", error);
      }
    };
    fetchUser();
  }, []);

  const toggleUser = (userId: number) => {
    if(!disabled){
      if (selected.includes(userId)) {
        onChange(selected.filter((id) => id !== userId));
      } else {
        onChange([...selected, userId]);
      }
    }
    
  };

  const removeUser = (userId: number) => {
    if (disabled) return;
    onChange(selected.filter((id) => id !== userId));
  };


  const selectedUsernames = selected
    .map((id) => user?.data.find((u) => u.id === id)?.username)
    .filter(Boolean) as string[];

  return (
    <div className="w-full  mx-auto relative">
      <label className="text-sm mb-1">
        Kullanıcılar
      </label>
      <div
      onClick={() => {
        if (!disabled) setIsOpen(!isOpen);
      }}
        className={`${
          disabled
            ? "border border-gray-300 rounded-md p-2 min-h-[42px] bg-white flex flex-wrap items-center gap-1 cursor-not-allowed"
            : "border border-gray-300 rounded-md p-2 min-h-[42px] bg-white flex flex-wrap items-center gap-1 cursor-pointer"
        }`}
      
      >
        {selected.map((id) => {
          const username = user?.data.find((u) => u.id === id)?.username;
          return (
            <span
              key={id}
              className="bg-teal-100 text-teal-800 text-sm px-2 py-1 rounded-full flex items-center"
            >
              {username}
            {!disabled && (
                   <button
                   onClick={(e) => {
                    e.stopPropagation();
                    removeUser(id);
                  }}
                  className="ml-1 text-xs font-bold"
                >
                  ✕
                </button>
            )}
            </span>
          );
        })}
        <div className="ml-auto">
          <svg
            className={`w-4 h-4 text-gray-500 transition-transform ${
              isOpen ? "rotate-180" : ""
            }`}
            fill="none"
            stroke="currentColor"
            strokeWidth={2}
            viewBox="0 0 24 24"
          >
            <path strokeLinecap="round" strokeLinejoin="round" d="M19 9l-7 7-7-7" />
          </svg>
        </div>
      </div>

      {isOpen && (
        <ul className="absolute w-full mt-1 bg-white border border-gray-200 rounded-md shadow-lg max-h-60 overflow-auto z-10">
          {user?.data.map((item) => (
            <li
              key={item.id}
              onClick={() => toggleUser(item.id)}
              className="cursor-pointer px-4 py-2 hover:bg-gray-100 flex justify-between items-center"
            >
              <span>{item.username}</span>
              {selected.includes(item.id) && (
                <svg
                  className="w-4 h-4 text-indigo-600"
                  fill="none"
                  stroke="currentColor"
                  strokeWidth={2}
                  viewBox="0 0 24 24"
                >
                  <path strokeLinecap="round" strokeLinejoin="round" d="M5 13l4 4L19 7" />
                </svg>
              )}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};
