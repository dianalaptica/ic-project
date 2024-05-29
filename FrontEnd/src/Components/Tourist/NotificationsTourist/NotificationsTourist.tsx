import React from "react";
import { useAuth } from "../../../Context/useAuth";
import SideBar from "./SideBar/SideBar";
import Body from "./Body Section/Body";

const NotificationsTourist = () => {
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
        <div>
          <p>123</p>
        </div>
      )}
    </>
  );
};

export default NotificationsTourist;
