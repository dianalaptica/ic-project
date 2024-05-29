import React from "react";
import SideBar from "./SideBar/SideBar";
import Body from "./Body Section/Body";
import "./SideBar/Sidebar.css";
import { useAuth } from "../../Context/useAuth";

const Test = () => {
  const { isLoggedIn } = useAuth();

  return (
    <>
      {/* {isLoggedIn() ? ( */}
      <div className="dashboard flex">
        <div className="dashboardContainer flex">
          <SideBar />
          <Body />
        </div>
      </div>
      {/* // ) : (
      //   <div></div>
      // )} */}
    </>
  );
};

export default Test;
