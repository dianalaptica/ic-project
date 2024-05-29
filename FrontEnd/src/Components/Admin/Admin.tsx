import React from "react";
import { useAuth } from "../../Context/useAuth";
import "../../App.css";
import SideBar from "./SideBar/SideBar";
import Body from "./Body Section/Body";

const Admin = () => {
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

export default Admin;
