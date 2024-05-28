import "./Body.css";
import useAxiosPrivate from "../../../../Hooks/useAxiosPrivate.ts";
import { useEffect, useState } from "react";
import { Trips } from "../../../../Models/Trips.ts";
import Top from "./Top Section/Top.tsx";
import default_trip_pic from "../../../../LoginAssets/default_trip_picture.png";

const Body = () => {
  const axiosPrivate = useAxiosPrivate();
  const [trips, setTrips] = useState<Trips>();

  const getAllTrips = async () => {
    const response = await axiosPrivate.get(`trips?hasJoined=${true}`);
    setTrips(response.data);
    console.log(response.data);
  };

  const removeUserFromTrip = async (tripId: number) => {
    const responsePatch = await axiosPrivate.patch(`trips/remove/${tripId}`);
    // after we update we force a rerender by making a new get req
    const responseGet = await axiosPrivate.get(`trips?hasJoined=${true}`);
    setTrips(responseGet.data);
  };

  useEffect(() => {
    getAllTrips();
  }, []);

  function formatDateString(inputDate: string) {
    const date = new Date(inputDate);
    const day = String(date.getDate()).padStart(2, "0");
    const month = String(date.getMonth() + 1).padStart(2, "0");
    const year = date.getFullYear();

    const hours = String(date.getHours()).padStart(2, "0");
    const minutes = String(date.getMinutes()).padStart(2, "0");
    const seconds = String(date.getSeconds()).padStart(2, "0");

    const formattedDate = `${day}-${month}-${year}, ${hours}:${minutes}:${seconds}`;

    return " " + formattedDate;
  }

  return (
    <div className="mainContent">
      <Top />
      <div className="bottom flex">
        {trips?.trips.map((elem) => (
          // <div key={elem.id}>
          //   <p>{elem.title}</p>
          //   <p>{elem.cityName}</p>
          //   <button onClick={async () => removeUserFromTrip(elem.id)}>
          //     RemoveFromTrip
          //   </button>
          //   <p>--------------------</p>
          //   </div>
          <div className="card card-side bg-base-100 shadow-xl">
            <figure className="fig">
              <img src={default_trip_pic} alt="Movie" />
            </figure>
            <div className="card-body">
              <h2 className="card-title">{elem.title}</h2>
              <p>{elem.description}</p>
              <p>Location: {elem.cityName}</p>
              <p>Adress: {elem.adress}</p>
              <p>
                Start Date:
                {formatDateString(elem.startDate.toString())}
              </p>
              <p>End Date: {formatDateString(elem.endDate.toString())}</p>
              <div className="card-actions justify-end">
                <button
                  className="btn"
                  onClick={async () => removeUserFromTrip(elem.id)}
                >
                  Remove me from trip
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>
      {/* <div className="card card-side bg-base-100 shadow-xl">
        <figure className="fig">
          <img src={default_trip_pic} alt="Movie" />
        </figure>
        <div className="card-body">
          <h2 className="card-title">New movie is released!</h2>
          <p>Click the button to watch on Jetflix app.</p>
          <div className="card-actions justify-end">
            <button className="btn btn-primary">Watch</button>
          </div>
        </div>
      </div> */}
    </div>
  );
};

export default Body;
