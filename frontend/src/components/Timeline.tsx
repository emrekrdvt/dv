import React, { useEffect, useState } from 'react';
import { HistoryResponse } from '../types/history';
import { getHistory } from '../services/Api';

interface TimelineProps {
  projectId: number;
}

const MyTimeline: React.FC<TimelineProps> = ({ projectId }) => {
  const [history, setHistory] = useState<HistoryResponse>();

  useEffect(() => {
    const fetchHistory = async () => {
      try {
        const response = await getHistory(projectId);
        const res: HistoryResponse = {
          success: response.success,
          message: response.message,
          data: response.data.sort((a, b) => new Date(b.timestamp).getTime() - new Date(a.timestamp).getTime()),
        };
        setHistory(res);
        console.log(res);
      } catch (error) {
        console.error('Error fetching history:', error);
      }
    };
    fetchHistory();
  }, [projectId]);

  return (
    <div className="max-h-[500px] overflow-y-auto p-10">
      <ol className="relative border-s border-gray-200 dark:border-gray-700">
        {
          history?.data.map((item, index) => (
            <li key={index} className="mb-10 ms-6">
              <span className="absolute flex items-center justify-center w-6 h-6 bg-blue-100 rounded-full -start-3 ring-8 ring-white dark:ring-gray-900 dark:bg-blue-900">
                <img
                  className="object-cover w-full h-full rounded-full shadow-lg"
                  src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQD3Q3c3bcDQBEO7i_OLPd5xcfPfRuKd2VWCA&s"
                  alt="Morty"
                />
              </span>
              <div className="p-4 bg-white border border-gray-200 rounded-lg shadow-xs dark:bg-gray-700 dark:border-gray-600">
                <div className="items-center justify-between mb-3 sm:flex">
                  <time className="mb-1 text-xs font-normal text-gray-400 sm:order-last sm:mb-0">{item.timestamp}</time>
                  <div className="text-sm font-normal text-gray-500 lex dark:text-gray-300">{item.missionTitle} # Başlıklı görevde değişiklik yapıldı : <a className="font-semibold text-gray-900 dark:text-white hover:underline">{item.username ?? " "} </a></div>
                </div>
                <div className="p-3 text-xs italic font-normal text-gray-500 border border-gray-200 rounded-lg bg-gray-50 dark:bg-gray-600 dark:border-gray-500 dark:text-gray-300">{item.action ?? " "}</div>
              </div>
            </li>
          ))
        }
      </ol>
    </div>


  );
}
export default MyTimeline;