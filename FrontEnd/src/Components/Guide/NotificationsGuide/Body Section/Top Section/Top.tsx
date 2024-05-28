import img from "../../../../../LoginAssets/no_profile_pic.png";
import video from "../../../../../LoginAssets/video.mp4";
import pisa from "../../../../../LoginAssets/pisa.png";
import "./Top.css";
import { useAuth } from "../../../../../Context/useAuth";
import { useNavigate } from "react-router-dom";

const Top = () => {
  const navigate = useNavigate();
  const { logout } = useAuth();

  const name = "Name";
  return (
    <div className="topSection">
      <div className="headerSection flex">
        <div className="title">
          <h1>Welcome to Guided.</h1>
          <p>Hello, {name}!</p>
        </div>

        <div className="adminDiv flex">
          <button className="btn" onClick={logout}>
            Log out
          </button>
          <div className="userImage">
            <img src={img} alt="User Image" />
          </div>
        </div>
      </div>
    </div>
  );
};

export default Top;
