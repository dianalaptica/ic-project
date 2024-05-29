import "./Body.css";
import useAxiosPrivate from "../../../../Hooks/useAxiosPrivate.ts";
import { useEffect, useState } from "react";
import { Trips } from "../../../../Models/Trips.ts";
import Top from "./Top Section/Top.tsx";
import default_trip_pic from "../../../../LoginAssets/default_trip_picture.png";

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
  const [trips, setTrips] = useState<Trips>();

  const callApi = async (cityId: number) => {
    try {
      const response = await axiosPrivate.get(`/trips?cityId=${cityId}`);
      console.log(response.data); // TODO: from here somehow render the components
      // TODO: the endpoint return Not Found if there are no trips and/or the city does not exist
      // London and Berlin is in this scenario
      setTrips(response.data);
    } catch (err) {
      console.log(err);
    }
  };

  const addUserToTrip = async (tripId: number) => {
    const responsePatch = await axiosPrivate.patch(`trips/join/${tripId}`);
    // after we update we force a rerender by making a new get req
    callApi(9);
  };

  useEffect(() => {
    callApi(8);
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
        {trips?.trips && trips.trips.length > 0 ? (
          trips.trips.map((elem) => {
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
                      className="btn"
                      onClick={async () => addUserToTrip(elem.id)}
                    >
                      Add me to trip
                    </button>
                  </div>
                </div>
              </div>
            );
          })
        ) : (
          <h2>Oops... No trips available for this location</h2>
        )}
      </div>
    </div>
  );
};

export default Body;
