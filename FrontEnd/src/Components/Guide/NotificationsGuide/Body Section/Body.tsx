import Top from "./Top Section/Top";
import "./Body.css";
import useAxiosPrivate from "../../../../Hooks/useAxiosPrivate.ts";
import { useEffect, useState } from "react";
import { GuideNotification } from "../../../../Models/GuideNotification.ts";

const Body = () => {
  const axiosPrivate = useAxiosPrivate();
  const [pastNotifications, setPastNotification] =
    useState<GuideNotification[]>();
  const [upcomingNotifications, setUpcomingNotification] =
    useState<GuideNotification[]>();

  const getAllPastNotifications = async () => {
    try {
      const response = await axiosPrivate.get(
        `notification/guide?isUpcoming=${false}`
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
      const response = await axiosPrivate.get(`notification/guide`);
      if (response.status === 200) {
        setUpcomingNotification(response.data);
      } else {
        setUpcomingNotification([]);
      }
    } catch (err) {
      setUpcomingNotification([]);
    }
  };

  const deleteNotification = async (id: number) => {
    const responseDelete = await axiosPrivate.delete(`notification/${id}`);
    await getAllUpcomingNotifications();
  };

  useEffect(() => {
    getAllPastNotifications();
    getAllUpcomingNotifications();
  }, []);

  return (
    <div className="mainContent">
      <Top />
      <div className="bottom flex">notifications guide</div>
      {pastNotifications?.map((elem) => (
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
