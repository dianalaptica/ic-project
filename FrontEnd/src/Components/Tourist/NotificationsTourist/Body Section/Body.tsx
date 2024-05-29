import "./Body.css";
import useAxiosPrivate from "../../../../Hooks/useAxiosPrivate.ts";
import { useEffect, useState } from "react";
import { UserNotification } from "../../../../Models/UserNotification.ts";
import Top from "./Top Section/Top.tsx";

const Body = () => {
  const axiosPrivate = useAxiosPrivate();
  const [notification, setNotification] = useState<UserNotification[]>([]);

  const updateReadStatus = async (id: number) => {
    const response = await axiosPrivate.patch(`notification/user/${id}`);
    getNotifications();
  };

  const getNotifications = async () => {
    const response = await axiosPrivate.get("notification/user");
    setNotification(response.data);
    console.log(notification);
  };

  useEffect(() => {
    getNotifications();
  }, []);

  console.log(notification);

  // TODO: map all documents and display a component for each one, when clicked call the method
  // IDEE Se poate face ceva care daca isRead e false sa arate ca nu e citit si daca e true sa apara citit
  // Poate notificarea sa apare pe un Dialog screen pe care sa il poti inchide dupa usor
  return (
    <div className="mainContent">
      <Top />
      <h1>New notifications</h1>
      <div className="bottom flex">
        {notification.map(
          (elem) =>
            elem.isRead === false && (
              <div key={`${elem.notificationId}` + `${elem.tripId}`}>
                <div className="card w-96 bg-base-100 shadow-xl">
                  <div className="card-body">
                    <h2 className="card-title">{elem.title}</h2>
                    <br />
                    <p>{elem.message}</p>
                    <p>Trip: {elem.tripTitle}</p>
                    <p>{elem.isRead ?? "true"}</p>
                    <div className="card-actions justify-end">
                      <button
                        className="btn"
                        onClick={async () =>
                          updateReadStatus(elem.notificationId)
                        }
                      >
                        Mark as read
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            )
        )}
      </div>
      <h1>Read notifications</h1>
      <div className="bottom flex">
        {notification.map(
          (elem) =>
            elem.isRead === true && (
              <div key={`${elem.notificationId}` + `${elem.tripId}`}>
                <div className="card read w-96 bg-base-100 shadow-xl">
                  <div className="card-body">
                    <h2 className="card-title">{elem.title}</h2>
                    <p>{elem.message}</p>
                    <p>Trip: {elem.tripTitle}</p>
                    <p>{elem.isRead ?? "true"}</p>
                    <div className="card-actions justify-end">
                      <button
                        className="btn"
                        onClick={async () =>
                          updateReadStatus(elem.notificationId)
                        }
                      >
                        Mark as unread
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            )
        )}
      </div>
    </div>
  );
};

export default Body;
