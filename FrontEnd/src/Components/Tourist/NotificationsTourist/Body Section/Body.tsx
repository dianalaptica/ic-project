import "./Body.css";
import useAxiosPrivate from "../../../../Hooks/useAxiosPrivate.ts";
import { useEffect, useState } from "react";
import { UserNotification } from "../../../../Models/UserNotification.ts";
import Top from "./Top Section/Top.tsx";

const Body = () => {
  const axiosPrivate = useAxiosPrivate();
  const [pastNotification, setPastNotification] = useState<UserNotification[]>(
    []
  );
  const [upcomingNotifications, setUpcomingNotification] =
    useState<UserNotification[]>();

  const updateReadStatus = async (id: number) => {
    const response = await axiosPrivate.patch(
      `notification/user/${id}`,
      updateReadStatus
    );
    await getAllUpcomingNotifications();
  };

  const getAllPastNotifications = async () => {
    try {
      const response = await axiosPrivate.get(
        `notification/user?isUpcoming=${false}`
      );
      if (response.status === 200) {
        setPastNotification(response.data);
      } else {
        setPastNotification([]);
      }
    } catch (err) {
      setPastNotification([]);
    }
  };

  const getAllUpcomingNotifications = async () => {
    try {
      const response = await axiosPrivate.get(`notification/user`);
      if (response.status === 200) {
        setUpcomingNotification(response.data);
      } else {
        setUpcomingNotification([]);
      }
    } catch (err) {
      setUpcomingNotification([]);
    }
  };

  useEffect(() => {
    getAllPastNotifications();
    getAllUpcomingNotifications();
  }, []);

  console.log(pastNotification);

  // TODO: map all documents and display a component for each one, when clicked call the method
  // IDEE Se poate face ceva care daca isRead e false sa arate ca nu e citit si daca e true sa apara citit
  // Poate notificarea sa apare pe un Dialog screen pe care sa il poti inchide dupa usor
  return (
    <div className="mainContent">
      <Top />
      <h1>New notifications</h1>
      {upcomingNotifications?.find((n) => n.isRead === false) ? (
        <div className="bottom flex">
          {upcomingNotifications?.map(
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
      ) : (
        <div>
          <br />
          <p>No unread notifications</p>
        </div>
      )}

      <h1>Read notifications</h1>
      {upcomingNotifications?.find((n) => n.isRead === true) ? (
        <div className="bottom flex">
          {upcomingNotifications?.map(
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
      ) : (
        <div>
          <br />
          <p>No read notifications</p>
        </div>
      )}

      <h1>Past notifications</h1>
      {pastNotification?.length > 0 ? (
        <div className="bottom flex">
          {pastNotification?.map((elem) => (
            <div key={`${elem.notificationId}` + `${elem.tripId}`}>
              <div className="card read w-96 bg-base-100 shadow-xl">
                <div className="card-body">
                  <h2 className="card-title">{elem.title}</h2>
                  <p>{elem.message}</p>
                  <p>Trip: {elem.tripTitle}</p>
                  <p>{elem.isRead ?? "true"}</p>
                </div>
              </div>
            </div>
          ))}
        </div>
      ) : (
        <div>
          <br />
          <p>No past notifications</p>
        </div>
      )}
    </div>
  );
};

export default Body;
