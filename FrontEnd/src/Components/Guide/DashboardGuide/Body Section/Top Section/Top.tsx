import img from "../../../../../LoginAssets/no_profile_pic.png";
import video from "../../../../../LoginAssets/video.mp4";
import pisa from "../../../../../LoginAssets/pisa.png";
import "./Top.css";
import { useAuth } from "../../../../../Context/useAuth";
import { useNavigate } from "react-router-dom";
import useAxiosPrivate from "../../../../../Hooks/useAxiosPrivate.ts";
import {useEffect, useState} from "react";
import {StatsModel} from "../../../../../Models/StatsModel.ts";

const Top = () => {
  const navigate = useNavigate();
  const { logout, user } = useAuth();
  const axiosPrivate = useAxiosPrivate();

  const [stats, setStats] = useState<StatsModel>({tripsToday: 0, tripsThisMonth: 0});

  const getStats = async () => {
    const response = await axiosPrivate.get("user/guide/stats")
    setStats(response.data)
  }

  useEffect(() => {
    getStats()
  }, []);

  const name = user?.firstName;
  return (
    <div className="topSection">
      <div className="headerSection flex">
        <div className="title">
          <h1>Welcome to Guided.</h1>
          <p>Hello, {name}!</p>
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

          <div className="buttons flex">
            <button
              className="btn transparent"
              onClick={() => navigate("/guide/my-trips")}
            >
              Create Trips
            </button>
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
                  Today <br /> <small>{stats.tripsToday} Trips</small>
                </span>
                <span>
                  This Month <br /> <small>{stats.tripsThisMonth} Trips</small>
                </span>
              </div>
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
