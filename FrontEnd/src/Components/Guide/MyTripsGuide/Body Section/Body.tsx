import Top from "./Top Section/Top";
import "./Body.css";
import useAxiosPrivate from "../../../../Hooks/useAxiosPrivate.ts";
import { useEffect, useState } from "react";
import { Trips } from "../../../../Models/Trips.ts";

const Body = () => {
  const axiosPrivate = useAxiosPrivate();
  const [pastTrips, setPastTrips] = useState<Trips>();
  const [upcomingTrips, setUpcomingTrips] = useState<Trips>();

  const getAllPastTrips = async () => {
    try {
      const response = await axiosPrivate.get(`trips?hasJoined=${true}&isUpcoming=${false}`);
      if (response.status === 200) {
        setPastTrips(response.data);
      } else {
        setPastTrips({hasNextPage: false, hasPreviousPage: false, page: 0, pageSize: 0, totalCount: 0, trips: []})
      }
    } catch (err) {
      setPastTrips({hasNextPage: false, hasPreviousPage: false, page: 0, pageSize: 0, totalCount: 0, trips: []})
    }
  };

  const getAllUpcomingTrips = async () => {
    try {
      const response = await axiosPrivate.get(`trips?hasJoined=${true}`);
      if (response.status === 200) {
        setUpcomingTrips(response.data);
      } else {
        setUpcomingTrips({hasNextPage: false, hasPreviousPage: false, page: 0, pageSize: 0, totalCount: 0, trips: []})
      }
    } catch (err) {
      setUpcomingTrips({hasNextPage: false, hasPreviousPage: false, page: 0, pageSize: 0, totalCount: 0, trips: []})
    }
  };

  const deleteTrip = async (id: number) => {
    const responseDelete = await axiosPrivate.delete(`trips/${id}`);
    await getAllUpcomingTrips();
  };

  useEffect(() => {
    getAllPastTrips();
    getAllUpcomingTrips();
  }, []);

  return (
    <div className="mainContent">
      <Top />
      <div className="bottom flex">trips guide</div>
      {pastTrips?.trips.map((elem) => (
        <div key={elem.id}>
          <p>{elem.title}</p>
          <p>{elem.adress}</p>
          <p>{elem.cityName}</p>
          <button onClick={() => deleteTrip(elem.id)}>DELETE</button>
        </div>
      ))}
    </div>
  );
};

export default Body;
