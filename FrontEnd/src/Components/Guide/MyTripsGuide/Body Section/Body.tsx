import Top from "./Top Section/Top";
import "./Body.css";
import useAxiosPrivate from "../../../../Hooks/useAxiosPrivate.ts";
import { useEffect, useState } from "react";
import { Trips } from "../../../../Models/Trips.ts";
import video from "../../../../LoginAssets/video.mp4";

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

  const deleteTrip = async (id: number) => {
    const responseDelete = await axiosPrivate.delete(`trips/${id}`);
    await getAllUpcomingTrips();
  };

  useEffect(() => {
    getAllPastTrips();
    getAllUpcomingTrips();
  }, []);

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
        {/* Open the modal using document.getElementById('ID').showModal() method */}
        <button
          className="btn"
          onClick={() =>
            document.getElementById("guide_create_trip").showModal()
          }
        >
          Create New Trip
        </button>
        <dialog id="guide_create_trip" className="modal">
          <div className="modal-box">
            <h3 className="font-bold text-lg">Create a new Trip!</h3>
            <p className="py-4">
              Press ESC key or click the button below to close
            </p>
            <p className="py-4">
              Press ESC key or click the button below to close
            </p>
            <p className="py-4">
              Press ESC key or click the button below to close
            </p>
            <p className="py-4">
              Press ESC key or click the button below to close
            </p>
            <div className="modal-action">
              <button className="btn submitTrip">Submit</button>
              <form method="dialog">
                {/* if there is a button in form, it will close the modal */}
                <button className="btn">Close</button>
              </form>
            </div>
          </div>
        </dialog>
        <h2>Upcoming Trips</h2>

        {upcomingTrips?.trips && upcomingTrips.trips.length > 0 ? (
          upcomingTrips.trips.map((elem) => {
            return (
              <div
                key={elem.id}
                className="card card-side bg-base-100 shadow-xl"
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
                  <p>
                    Start Date: {formatDateString(elem.startDate.toString())}
                  </p>
                  <p>End Date: {formatDateString(elem.endDate.toString())}</p>
                  <p className="pColor">
                    {elem.maxTourists !== 0 ? elem.maxTourists : 0} places left
                  </p>
                  <div className="card-actions justify-end">
                    <button
                      className="btn deleteTrip"
                      onClick={async () => deleteTrip(elem.id)}
                    >
                      Delete Trip
                    </button>
                  </div>
                </div>
              </div>
            );
          })
        ) : (
          <div className="cardSection flex">
            <div className="rightCard flex">
              <h1>No available trips at the moment</h1>
              <br />

              <div className="videoDiv">
                <video src={video} autoPlay loop muted></video>
              </div>
            </div>
          </div>
        )}

        <div>
          {trips?.trips.map((elem) => (
            <div key={elem.id}>
              <p>{elem.title}</p>
              <p>{elem.adress}</p>
              <p>{elem.cityName}</p>
              <button onClick={() => deleteTrip(elem.id)}>DELETE</button>
            </div>
          ))}
        </div>
      </div>
      <br />
    </div>
  );
};

export default Body;
