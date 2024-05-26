import "./Body.css";
import useAxiosPrivate from "../../../../Hooks/useAxiosPrivate.ts";
import {useEffect, useState} from "react";
import {UserNotification} from "../../../../Models/UserNotification.ts";

const Body = () => {
    const axiosPrivate = useAxiosPrivate();
    const [notification, setNotification] = useState<UserNotification[]>([]);

    const updateReadStatus = async (id: number) => {
        const response = await axiosPrivate.patch(`notification/user/${id}`)
    }

    const getNotifications = async () => {
        const response = await axiosPrivate.get("notification/user")
        setNotification(response.data)
        console.log(notification)
    }
    
    useEffect(() => {
        getNotifications()
    }, [])

    console.log(notification)

    // TODO: map all documents and display a component for each one, when clicked call the method
    // IDEE Se poate face ceva care daca isRead e false sa arate ca nu e citit si daca e true sa apara citit
    // Poate notificarea sa apare pe un Dialog screen pe care sa il poti inchide dupa usor
    return (
        <div className="mainContent">
            notification
            <div className="bottom flex">
                {notification.map((elem) =>
                <div key={`${elem.notificationId}` + `${elem.tripId}`}>
                    <button onClick={async () => updateReadStatus(elem.notificationId)}>{elem.title}</button>
                    <p>{elem.message}</p>
                    <p>{elem.isRead ? "TRUE" : "FALSE"}</p>
                </div>)}
            </div>
        </div>
    );
};

export default Body;
