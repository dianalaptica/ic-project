import "./Body.css";
import useAxiosPrivate from "../../../../Hooks/useAxiosPrivate.ts";
import { useEffect, useState } from "react";
import { Trips } from "../../../../Models/Trips.ts";
import Top from "./Top Section/Top.tsx";
import video from "../../../../LoginAssets/video.mp4";

function base64ToBlob(base64String: string, contentType: string) {
  contentType = contentType || "";
  const byteCharacters = atob(base64String);
  const byteNumbers = new Array(byteCharacters.length);

  for (let i = 0; i < byteCharacters.length; i++) {
    byteNumbers[i] = byteCharacters.charCodeAt(i);
  }

  const byteArray = new Uint8Array(byteNumbers);
  return new Blob([byteArray], { type: contentType });
}

const Body = () => {
  const axiosPrivate = useAxiosPrivate();
  const [pastTrips, setPastTrips] = useState<Trips>();
  const [upcomingTrips, setUpcomingTrips] = useState<Trips>();

  const getAllPastTrips = async () => {
    try {
      const response = await axiosPrivate.get(
        `trips?hasJoined=${true}&isUpcoming=${false}`
      );
      if (response.status === 200) {
        setPastTrips(response.data);
      } else {
        setPastTrips({
          hasNextPage: false,
          hasPreviousPage: false,
          page: 0,
          pageSize: 0,
          totalCount: 0,
          trips: [],
        });
      }
    } catch (err) {
      setPastTrips({
        hasNextPage: false,
        hasPreviousPage: false,
        page: 0,
        pageSize: 0,
        totalCount: 0,
        trips: [],
      });
    }
  };

  const getAllUpcomingTrips = async () => {
    try {
      const response = await axiosPrivate.get(`trips?hasJoined=${true}`);
      if (response.status === 200) {
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

  const removeUserFromTrip = async (tripId: number) => {
    const responsePatch = await axiosPrivate.patch(`trips/remove/${tripId}`);
    // after we update we force a rerender by making a new get req
    await getAllUpcomingTrips();
  };

  useEffect(() => {
    getAllPastTrips();
    getAllUpcomingTrips();
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

  const createImageSrc = (imageFile) => {
    return URL.createObjectURL(base64ToBlob(imageFile, "image/png"));
  };

  return (
    <div className="mainContent">
      <Top />

      <div className="bottomTripTourist flex">
        <h2>My upcoming trips</h2>
        {upcomingTrips?.trips && upcomingTrips.trips.length > 0 ? (
          upcomingTrips.trips.map((elem) => (
            <div key={elem.id} className="card card-side bg-base-100 shadow-xl">
              <figure className="fig">
                <img src={createImageSrc(elem.image)} alt="Trip" />
              </figure>
              <div className="card-body">
                <h2 className="card-title">{elem.title}</h2>
                <p>{elem.description}</p>
                <p>
                  Location: {elem.cityName}, {elem.countryName}
                </p>
                <p>Address: {elem.adress}</p>
                <p>Start Date: {formatDateString(elem.startDate.toString())}</p>
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
          ))
        ) : (
          <div className="cardSection flex">
            <div className="rightCard flex">
              <h1>You don't have any trips booked at the moment</h1>
              <br />

              <div className="videoDiv">
                <video src={video} autoPlay loop muted></video>
              </div>
            </div>
          </div>
        )}
        {/* </div> */}
        {/* <div className="bottomTripTourist flex"> */}
        <h2>My past trips</h2>
        {pastTrips?.trips && pastTrips.trips.length > 0 ? (
          pastTrips.trips.map((elem) => (
            <div
              key={elem.id}
              className="card read card-side bg-base-100 shadow-xl"
            >
              <figure className="fig">
                <img src={createImageSrc(elem.image)} alt="Trip" />
              </figure>
              <div className="card-body">
                <h2 className="card-title">{elem.title}</h2>
                <p>{elem.description}</p>
                <p>
                  Location: {elem.cityName}, {elem.countryName}
                </p>
                <p>Address: {elem.adress}</p>
                <p>Start Date: {formatDateString(elem.startDate.toString())}</p>
                <p>End Date: {formatDateString(elem.endDate.toString())}</p>
              </div>
            </div>
          ))
        ) : (
          <p>No past trips</p>
        )}
      </div>
    </div>
  );
};

export default Body;
