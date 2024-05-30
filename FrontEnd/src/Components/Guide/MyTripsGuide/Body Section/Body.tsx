import Top from "./Top Section/Top";
import "./Body.css";
import useAxiosPrivate from "../../../../Hooks/useAxiosPrivate.ts";
import { useEffect, useState } from "react";
import { Trips } from "../../../../Models/Trips.ts";

const Body = () => {
  const axiosPrivate = useAxiosPrivate();
  const [trips, setTrips] = useState<Trips>();

  const getAllTrips = async () => {
    const response = await axiosPrivate.get(`trips?hasJoined=${true}`);
    setTrips(response.data);
    console.log(response.data);
  };

  const deleteTrip = async (id: number) => {
    const responseDelete = axiosPrivate.delete(`trips/${id}`);
    const responseGet = await axiosPrivate.get(`trips?hasJoined=${true}`);
    if (responseGet.status === 404) {
      setTrips({
        hasNextPage: false,
        hasPreviousPage: false,
        page: 0,
        pageSize: 0,
        totalCount: 0,
        trips: [],
      });
    } else {
      setTrips(responseGet.data);
    }
  };

  useEffect(() => {
    getAllTrips();
  }, []);

  return (
    <div className="mainContent">
      <Top />
      <div className="bottom flex">
        <button className="btn">Create New Trip</button>
      </div>
      <br />
      {trips?.trips.map((elem) => (
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
