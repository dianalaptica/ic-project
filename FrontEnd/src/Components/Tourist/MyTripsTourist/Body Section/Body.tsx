import "./Body.css";
import useAxiosPrivate from "../../../../Hooks/useAxiosPrivate.ts";
import {useEffect, useState} from "react";
import {Trips} from "../../../../Models/Trips.ts";

const Body = () => {
    const axiosPrivate = useAxiosPrivate()
    const [trips, setTrips] = useState<Trips>()

    const getAllTrips = async () => {
        const response = await axiosPrivate.get(`trips?hasJoined=${true}`)
        setTrips(response.data)
        console.log(response.data)
    }

    const removeUserFromTrip = async (tripId: number) => {
        const responsePatch = await axiosPrivate.patch(`trips/remove/${tripId}`)
        // after we update we force a rerender by making a new get req
        const responseGet = await axiosPrivate.get(`trips?hasJoined=${true}`)
        setTrips(responseGet.data)
    }

    useEffect(() => {
        getAllTrips()
    }, [])

  return (
    <div className="mainContent">

        {trips?.trips.map((elem) =>
            <div key={elem.id}>
                <p>{elem.title}</p>
                <p>{elem.cityName}</p>
                <button onClick={async () => removeUserFromTrip(elem.id)}>RemoveFromTrip</button>
                <p>--------------------</p>
            </div>)}
    </div>
  );
};

export default Body;
