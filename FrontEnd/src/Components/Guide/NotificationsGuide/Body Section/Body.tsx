import Top from "./Top Section/Top";
import "./Body.css";
import useAxiosPrivate from "../../../../Hooks/useAxiosPrivate.ts";
import {useEffect, useState} from "react";
import {GuideNotification} from "../../../../Models/GuideNotification.ts";

const Body = () => {
    const axiosPrivate = useAxiosPrivate();
    const [notifications, setNotification] = useState<GuideNotification[]>();

    const getAllNotifications = async () => {
        const response = await axiosPrivate.get(`notification/guide`);
        setNotification(response.data);
        console.log(response.data);
    };

    const deleteNotification = async (id: number) => {
        console.log(id)
        const responseDelete = await axiosPrivate.delete(`notification/${id}`)
        const responseGet = await axiosPrivate.get(`notification/guide`);
        if (responseGet.status === 404) {
            setNotification([])
        } else {
            setNotification(responseGet.data);
        }
    }

    useEffect(() => {
        getAllNotifications();
    }, []);

  return (
    <div className="mainContent">
      <Top />
      <div className="bottom flex">notifications guide</div>
        {notifications?.map((elem) => (
            <div key={elem.id}>
                <p>{elem.tripTitle}</p>
                <p>{elem.title}</p>
                <p>{elem.message}</p>
                <button onClick={() => deleteNotification(elem.id)}>DELETE</button>
            </div>
        ))}
    </div>
  );
};

export default Body;
