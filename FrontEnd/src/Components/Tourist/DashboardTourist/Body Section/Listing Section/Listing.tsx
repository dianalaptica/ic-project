import React from "react";
import "./Listing.css";
import { BsArrowRightShort } from "react-icons/bs";
import paris from "../../../../../LoginAssets/paris.png";
import london from "../../../../../LoginAssets/london.png";
import rome from "../../../../../LoginAssets/rome.png";
import berlin from "../../../../../LoginAssets/berlin.png";
import img from "../../../../../LoginAssets/no_profile_pic.png";
import Trip from "../Trip/Trip.tsx";
import useAxiosPrivate from "../../../../../Hooks/useAxiosPrivate.ts";

const Listing = () => {
  const axiosPrivate = useAxiosPrivate();

  const callApi = async (cityId: number) => {
    try {
      const response = await axiosPrivate.get(`/trips?cityId=${cityId}`);
      console.log(response.data); // TODO: from here somehow render the components
      // TODO: the endpoint return Not Found if there are no trips and/or the city does not exist
      // London and Berlin is in this scenario
    } catch (err) {
      console.log(err);
    }
  };

  return (
    <div className="listingSection">
      <div className="heading flex">
        <h1>See trips from popular locations</h1>
        <button className="btn flex">
          See All <BsArrowRightShort className="icon" />
        </button>
      </div>

      <div className="secContainerTourist flex">
        <div
          className="singleItem"
          onClick={async () => {
            await callApi(14);
          }}
        >
          {/* <AiFillHeart className="icon" /> */}
          <img src={paris} alt="Eiffel Tower" />
          <h3>Paris, FR</h3>
        </div>

        <div
          className="singleItem"
          onClick={async () => {
            await callApi(17);
          }}
        >
          {/* <AiOutlineHeart className="icon" /> */}
          <img src={london} alt="Big Ben" />
          <h3>London, UK</h3>
        </div>

        <div
          className="singleItem"
          onClick={async () => {
            await callApi(9);
          }}
        >
          {/* <AiOutlineHeart className="icon" /> */}
          <img src={rome} alt="Colosseum" />
          <h3>Rome, IT</h3>
        </div>

        <div
          className="singleItem"
          onClick={async () => {
            await callApi(8);
          }}
        >
          {/* <AiFillHeart className="icon" /> */}
          <img src={berlin} alt="Berlin" />
          <h3>Berlin, D</h3>
        </div>
      </div>

      <div className="guides flex">
        <div className="topGuides">
          <div className="heading flex">
            <h3>Guides</h3>
            <button className="btn flex">
              See All <BsArrowRightShort className="icon" />
            </button>
          </div>

          <div className="card flex">
            <div className="users">
              <img src={img} alt="User Image" />
              <img src={img} alt="User Image" />
              <img src={img} alt="User Image" />
              <img src={img} alt="User Image" />
            </div>
            <div className="cardText">
              <span>
                15 Trips <br />
                <small>4 guides</small>
              </span>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Listing;
