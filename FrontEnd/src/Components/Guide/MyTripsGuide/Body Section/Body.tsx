import Top from "./Top Section/Top";
import "./Body.css";
import useAxiosPrivate from "../../../../Hooks/useAxiosPrivate.ts";
import { useEffect, useState } from "react";
import { Trips } from "../../../../Models/Trips.ts";
import { useForm } from "react-hook-form";

type CreateTripForm = {
  title: string;
  description: string;
  adress: string;
  startDate: Date;
  endDate: Date;
  maxTourists: number;
  image: Blob;
};

const Body = () => {
  const axiosPrivate = useAxiosPrivate();
  const [pastTrips, setPastTrips] = useState<Trips>();
  const [upcomingTrips, setUpcomingTrips] = useState<Trips>();
  const { register, handleSubmit } = useForm<CreateTripForm>({});

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

  function parseDateString(dateString: string) {
    const [datePart, timePart] = dateString.split(" ");

    const [day, month, year] = datePart.split(".").map(Number);

    const [hours, minutes, seconds] = timePart.split(":").map(Number);

    const date = new Date(year, month - 1, day, hours, minutes, seconds);

    return date;
  }

  const handleCreateTrip = async (form: CreateTripForm) => {
    console.log(form.title);
    console.log(form.description);
    console.log(form.adress);
    console.log(parseDateString(form.startDate));
    console.log(form.endDate);
    console.log(form.maxTourists);
    console.log(form.image);
  };

  return (
    <div className="mainContent">
      <Top />
      <div className="bottomTripTourist flex">
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

            <form
              action=""
              className="form grid"
              onSubmit={handleSubmit(handleCreateTrip)}
            >
              <div className="inputDiv">
                <label htmlFor="title">Title</label>
                <div className="input flex">
                  <input
                    type="text"
                    id="title"
                    placeholder="Enter trip title"
                    {...register("title")}
                  />
                </div>
              </div>

              <div className="inputDiv">
                <label htmlFor="description">Description</label>
                <div className="input flex">
                  <input
                    type="text"
                    id="description"
                    placeholder="Enter trip description"
                    {...register("description")}
                  />
                </div>
              </div>

              <div className="inputDiv">
                <label htmlFor="adress">Adress</label>
                <div className="input flex">
                  <input
                    type="text"
                    id="adress"
                    placeholder="Enter the adress of the trip"
                    {...register("adress")}
                  />
                </div>
              </div>

              <div className="inputDiv">
                <label htmlFor="startDate">Start Date</label>
                <div className="input flex">
                  <input
                    type="text"
                    id="startDate"
                    placeholder="Eg. 01.06.2024 16:30:00"
                    {...register("startDate")}
                  />
                </div>
              </div>

              <div className="inputDiv">
                <label htmlFor="endDate">End Date</label>
                <div className="input flex">
                  <input
                    type="text"
                    id="endDate"
                    placeholder="Eg. 01.06.2024 17:30:00"
                    {...register("endDate")}
                  />
                </div>
              </div>

              <div className="inputDiv">
                <label htmlFor="maxTourists">Max Tourists</label>
                <div className="input flex">
                  <input
                    type="number"
                    id="maxTourists"
                    placeholder="Enter the max number of tourists"
                    {...register("maxTourists")}
                  />
                </div>
              </div>

              <label htmlFor="image">Trip Image</label>
              <input
                type="file"
                className="file-input file-input-bordered w-full max-w-xs"
                {...register("image")}
              />
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
          <p>No upcoming trips</p>
        )}

        <h2>Past Trips</h2>

        {pastTrips?.trips && pastTrips.trips.length > 0 ? (
          pastTrips.trips.map((elem) => {
            return (
              <div
                key={elem.id}
                className="card past card-side bg-base-100 shadow-xl"
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
                </div>
              </div>
            );
          })
        ) : (
          <p>No past trips</p>
        )}
      </div>
      <br />
    </div>
  );
};

export default Body;
