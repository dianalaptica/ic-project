import { useState } from "react";
import "./Sidebar.css";
import logo from "../../../LoginAssets/logo.png";
import { IoMdSpeedometer } from "react-icons/io";
import { useNavigate } from "react-router-dom";

const SideBar = () => {
  const navigate = useNavigate();
  const [activeMenu, setActiveMenu] = useState("applications");

  const handleMenuClick = (menu: string, route: string) => {
    setActiveMenu(menu);
    navigate(route);
  };

  return (
    <div className="sideBarAdmin grid">
      <div className="logoDiv flex">
        <img src={logo} alt="Guided Logo" />
        <h2>Guided.</h2>
      </div>

      <div className="menuDiv">
        <h3 className="divTitle">QUICK MENU</h3>
        <ul className="menuLists grid">
          <li
            className={`listItem ${
              activeMenu === "applications" ? "active" : ""
            }`}
          >
            <a
              href=""
              className="menuLink flex"
              onClick={() => handleMenuClick("dashboard", "/admin")}
            >
              <IoMdSpeedometer className="icon" />
              <span className="smallText">Applications</span>
            </a>
          </li>
        </ul>
      </div>
    </div>
  );
};

export default SideBar;
