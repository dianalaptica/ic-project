import Top from "./Top Section/Top";
import "./Body.css";
import useAxiosPrivate from "../../../../Hooks/useAxiosPrivate.ts";
import { useEffect, useState } from "react";
import { GuideNotification } from "../../../../Models/GuideNotification.ts";
import { useForm } from "react-hook-form";
import { formToJSON } from "axios";
import { Trips } from "../../../../Models/Trips.ts";
import { toast } from "react-toastify";

type CreateNotificationForm = {
  tripId: number;
  title: string;
  message: string;
};

const Body = () => {
  const axiosPrivate = useAxiosPrivate();
  const [pastNotifications, setPastNotification] =
    useState<GuideNotification[]>();
  const [upcomingNotifications, setUpcomingNotification] =
    useState<GuideNotification[]>();
  const [upcomingTrips, setUpcomingTrips] = useState<Trips>();
  const [trip, setTrip] = useState("Select a trip");

  const getAllUpcomingTrips = async () => {
    try {
      const response = await axiosPrivate.get(`trips?hasJoined=${true}`);
      if (response.status === 200) {
        console.log(upcomingTrips);
        setUpcomingTrips(response.data);
      } else {
        setUpcomingTrips({
          hasNextPage: false,
          hasPreviousPage: false,
          page: 0,
          pageSize: 0,
          totalCount: 0,
          trips: [],
        });
      }
    } catch (err) {
      setUpcomingTrips({
        hasNextPage: false,
        hasPreviousPage: false,
        page: 0,
        pageSize: 0,
        totalCount: 0,
        trips: [],
      });
    }
  };

  const { register, handleSubmit, setValue } = useForm<CreateNotificationForm>(
    {}
  );

  const selectTrip = (id: number, title: string) => {
    setTrip(title);
    setValue("tripId", id);
  };

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

  const handleCreateNotification = async (form: CreateNotificationForm) => {
    console.log(form.tripId);
    console.log(form.title);
    console.log(form.message);
    // daca response status ==- 200
    toast.success("Created notification!");
  };

  useEffect(() => {
    getAllPastNotifications();
    getAllUpcomingNotifications();
    getAllUpcomingTrips();
  }, []);

  return (
    <div className="mainContent">
      <Top />
      <div className="bottomNotifGuide flex">
        <button
          className="btn"
          onClick={() =>
            document.getElementById("guide_create_notif").showModal()
          }
        >
          Create New Notification
        </button>
        <dialog id="guide_create_notif" className="modal">
          <div className="modal-box">
            <h3 className="font-bold text-lg">Create a new Notification!</h3>

            <form
              action=""
              className="form grid"
              onSubmit={handleSubmit(handleCreateNotification)}
            >
              <div className="inputDiv">
                <label htmlFor="title">Title</label>
                <div className="input flex">
                  <input
                    type="text"
                    id="title"
                    placeholder="Enter notification title"
                    {...register("title")}
                  />
                </div>
              </div>

              <div className="inputDiv">
                <label htmlFor="message">Message</label>
                <div className="input flex">
                  <input
                    type="text"
                    id="message"
                    placeholder="Enter notification message"
                    {...register("message")}
                  />
                </div>
              </div>

              <div className="inputDiv">
                <label htmlFor="trip">Trip</label>
                <div className="input flex">
                  <details className="dropdown">
                    <summary className="m-1 ">{trip}</summary>
                    <ul className="p-2 shadow menu dropdown-content z-[1] bg-base-100 rounded-box w-52">
                      {upcomingTrips?.trips.map((elem) => (
                        <li key={elem.id}>
                          <a onClick={() => selectTrip(elem.id, elem.title)}>
                            {elem.title}
                          </a>
                        </li>
                      ))}
                    </ul>
                  </details>
                </div>
              </div>

              <button type="submit" className="btn submitTrip">
                Submit
              </button>
            </form>

            <div className="modal-action">
              <form method="dialog">
                <button className="btn">Close</button>
              </form>
            </div>
          </div>
        </dialog>

        <h2>Notifications for upcoming trips</h2>

        {upcomingNotifications && upcomingNotifications?.length > 0 ? (
          upcomingNotifications?.map((elem) => (
            <div key={elem.id} className="card read w-96 bg-base-100 shadow-xl">
              <div className="card-body">
                <h2 className="card-title">{elem.title}</h2>
                <p>{elem.message}</p>
                <p>Trip: {elem.tripTitle}</p>
                <div className="card-actions justify-end">
                  <button
                    className="btn"
                    onClick={async () => deleteNotification(elem.id)}
                  >
                    delete
                  </button>
                </div>
              </div>
            </div>
          ))
        ) : (
          <p>No past notifications</p>
        )}

        <h2>Notifications for past trips</h2>

        {pastNotifications && pastNotifications?.length > 0 ? (
          pastNotifications?.map((elem) => (
            <div
              key={elem.id}
              className="card pastNotif read w-96 bg-base-100 shadow-xl"
            >
              <div className="card-body">
                <h2 className="card-title">{elem.title}</h2>
                <p>{elem.message}</p>
                <p>Trip: {elem.tripTitle}</p>
              </div>
            </div>
          ))
        ) : (
          <p>No past notifications</p>
        )}
      </div>
    </div>
  );
};

export default Body;
