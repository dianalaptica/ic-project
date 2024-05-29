import { useState } from "react";
import "./Sidebar.css";
import logo from "../../../../LoginAssets/logo.png";
import { IoMdSpeedometer } from "react-icons/io";
import { LiaGlobeAmericasSolid } from "react-icons/lia";
import { MdOutlineManageAccounts } from "react-icons/md";
import { IoNotificationsCircleOutline } from "react-icons/io5";
import { useNavigate } from "react-router-dom";

const SideBar = () => {
  const navigate = useNavigate();
  const [activeMenu, setActiveMenu] = useState("myTrips");

  const handleMenuClick = (menu: string, route: string) => {
    setActiveMenu(menu);
    navigate(route);
  };

  return (
    <div className="sideBarGuide grid">
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
              onClick={() => handleMenuClick("dashboard", "/guide/dashboard")}
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
              onClick={() => handleMenuClick("myTrips", "/guide/my-trips")}
            >
              <LiaGlobeAmericasSolid className="icon" />
              <span className="smallText">My Trips</span>
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
                handleMenuClick("notifications", "/guide/notifications")
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
              onClick={() => handleMenuClick("myAccount", "/guide/account")}
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
    </div>
  );
};

export default SideBar;
