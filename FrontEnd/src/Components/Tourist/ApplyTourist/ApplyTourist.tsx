import React from "react";
import { useAuth } from "../../../Context/useAuth";
import SideBar from "./SideBar/SideBar";
import Body from "./Body Section/Body";

const ApplyTourist = () => {
  const { isLoggedIn } = useAuth();

  return (
    <>
      {isLoggedIn() ? (
        <div className="dashboard flex">
          <div className="dashboardContainer flex">
            <SideBar />
            <Body />
          </div>
        </div>
      ) : (
        <div></div>
      )}
    </>
  );
};

export default ApplyTourist;
