import React, { useState } from "react";
import "./Sidebar.css";
import logo from "../../../../LoginAssets/logo.png";
import { IoMdSpeedometer } from "react-icons/io";
import { LiaGlobeAmericasSolid } from "react-icons/lia";
import { MdOutlineExplore, MdOutlineManageAccounts } from "react-icons/md";
import { IoNotificationsCircleOutline } from "react-icons/io5";
import { HiUserGroup } from "react-icons/hi";
import { useNavigate } from "react-router-dom";

const SideBar = () => {
  const navigate = useNavigate();
  const [activeMenu, setActiveMenu] = useState("myTrips");

  const handleMenuClick = (menu: string, route: string) => {
    setActiveMenu(menu);
    navigate(route);
  };

  return (
    <div className="sideBar grid">
      <div className="logoDiv flex">
        <img src={logo} alt="Guided Logo" />
        <h2>Guided.</h2>
      </div>

      <div className="menuDiv">
        <h3 className="divTitle">QUICK MENU</h3>
        <ul className="menuLists grid">
          <li
            className={`listItem ${activeMenu === "dashboard" ? "active" : ""}`}
          >
            <a
              href=""
              className="menuLink flex"
              onClick={() => handleMenuClick("dashboard", "/tourist/dashboard")}
            >
              <IoMdSpeedometer className="icon" />
              <span className="smallText">Dashboard</span>
            </a>
          </li>

          <li
            className={`listItem ${activeMenu === "myTrips" ? "active" : ""}`}
          >
            <a
              href=""
              className="menuLink flex"
              onClick={() => handleMenuClick("myTrips", "/tourist/my-trips")}
            >
              <LiaGlobeAmericasSolid className="icon" />
              <span className="smallText">My Trips</span>
            </a>
          </li>

          <li
            className={`listItem ${activeMenu === "findTrips" ? "active" : ""}`}
          >
            <a
              href=""
              className="menuLink flex"
              onClick={() =>
                handleMenuClick("findTrips", "/tourist/find-trips")
              }
            >
              <MdOutlineExplore className="icon" />
              <span className="smallText">Find Trips</span>
            </a>
          </li>

          <li
            className={`listItem ${
              activeMenu === "notifications" ? "active" : ""
            }`}
          >
            <a
              href=""
              className="menuLink flex"
              onClick={() =>
                handleMenuClick("notifications", "/tourist/notifications")
              }
            >
              <IoNotificationsCircleOutline className="icon" />
              <span className="smallText">Notifications</span>
            </a>
          </li>
        </ul>
      </div>

      <div className="settingsDiv">
        <h3 className="divTitle">SETTINGS</h3>
        <ul className="menuLists grid">
          <li
            className={`listItem ${activeMenu === "myAccount" ? "active" : ""}`}
          >
            <a
              href=""
              className="menuLink flex"
              onClick={() => handleMenuClick("myAccount", "/tourist/account")}
            >
              <MdOutlineManageAccounts className="icon" />
              <span className="smallText">My Account</span>
            </a>
          </li>
          {/* <li className="listItem">
            <a href="#" className="menuLink flex">
              <MdOutlineManageAccounts className="icon" />
              <span className="smallText">My Account</span>
            </a>
          </li> */}
        </ul>
      </div>

      <div className="sideBarCard">
        <HiUserGroup className="icon" />
        <div className="cardContent">
          <div className="circle1"></div>
          <div className="circle2"></div>

          <h3>Want to be a guide?</h3>
          <p>
            Would you like to share with other people your favorites spots in
            the city?
          </p>
          <button className="btn" onClick={() => navigate("/tourist/apply")}>
            Apply Now
          </button>
        </div>
      </div>
    </div>
  );
};

export default SideBar;
