import "./Body.css";
import useAxiosPrivate from "../../../../Hooks/useAxiosPrivate.ts";
import { useEffect, useState } from "react";
import { Trips } from "../../../../Models/Trips.ts";
import Top from "./Top Section/Top.tsx";

const Body = () => {
  const axiosPrivate = useAxiosPrivate();
  const [trips, setTrips] = useState<Trips>();

  const getAllTrips = async () => {
    const response = await axiosPrivate.get(`trips?hasJoined=${false}`);
    setTrips(response.data);
    console.log(response.data);
  };

  const addUserToTrip = async (tripId: number) => {
    const responsePatch = await axiosPrivate.patch(`trips/join/${tripId}`);
    // after we update we force a rerender by making a new get req
    const responseGet = await axiosPrivate.get(`trips?hasJoined=${false}`);
    setTrips(responseGet.data);
  };

  useEffect(() => {
    getAllTrips();
  }, []);

  return (
    <div className="mainContent">
      <Top />
      <div className="bottom flex">
        {trips?.trips.map((elem) => (
          <div key={elem.id}>
            <p>{elem.title}</p>
            <p>{elem.cityName}</p>
            <button onClick={async () => addUserToTrip(elem.id)}>
              AddToTrip
            </button>
            <p>--------------------</p>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Body;
