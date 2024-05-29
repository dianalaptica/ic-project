import { BiSearchAlt } from "react-icons/bi";
import img from "../../../../../LoginAssets/no_profile_pic.png";
import video from "../../../../../LoginAssets/video.mp4";
import pisa from "../../../../../LoginAssets/pisa.png";
import "./Top.css";
import { BsArrowRightShort } from "react-icons/bs";
import { useAuth } from "../../../../../Context/useAuth";
import { useNavigate } from "react-router-dom";

const Top = () => {
  const navigate = useNavigate();
  const { logout, user } = useAuth();

  const name = user?.firstName;
  return (
    <div className="topSection">
      <div className="headerSection flex">
        <div className="title">
          <h1>Welcome to Guided.</h1>
          <p>Hello, {name}!</p>
        </div>

        <div className="searchBar flex">
          <input type="text" placeholder="Search..." />
          <BiSearchAlt className="icon" />
        </div>

        <div className="adminDiv flex">
          {/* <TbMessageCircle className="icon" />
          <MdOutlineNotificationsNone className="icon" /> */}
          <button className="btn" onClick={logout}>
            Log out
          </button>
          <div className="userImage">
            <img src={img} alt="User Image" />
          </div>
        </div>
      </div>

      <div className="cardSection flex">
        <div className="rightCard flex">
          <h1>Create extraordinary memories</h1>
          <br />
          {/* <p>Lalala alallala ldkdkks kkdjdjdjdk jdjdjdjsjmd</p> */}

          <div className="buttons flex">
            <button
              className="btn"
              onClick={() => navigate("/tourist/find-trips")}
            >
              Explore Trips
            </button>
            <button className="btn transparent">See Guides</button>
          </div>

          <div className="videoDiv">
            <video src={video} autoPlay loop muted></video>
          </div>
        </div>

        <div className="leftCard flex">
          <div className="main flex">
            <div className="textDiv">
              <h1>My Stat</h1>

              <div className="flex">
                <span>
                  Today <br /> <small>2 Trips</small>
                </span>
                <span>
                  This Month <br /> <small>8 Trips</small>
                </span>
              </div>

              <span
                className="flex link"
                onClick={() => navigate("/tourist/my-trips")}
              >
                Go to my trips <BsArrowRightShort className="icon" />
              </span>
            </div>

            <div className="imgDiv">
              <img src={pisa} alt="Pisa Tower" />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Top;
