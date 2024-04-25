import React from "react";
import SideBar from "./SideBar/SideBar";
import Body from "./Body Section/Body";
import "./SideBar/Sidebar.css";

const Test = () => {
  return (
    // <div className="dashboard flex">
    //   <div className="dashboardContainer flex">
    //     <SideBar />
    //     <Body />
    //   </div>
    // </div>
    <div className="container">
      <SideBar />
      <Body />
    </div>
  );
};

export default Test;
