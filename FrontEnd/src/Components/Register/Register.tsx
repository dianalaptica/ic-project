import "./Register.css";
import "../../App.css";
import video from "../../LoginAssets/video.mp4";
import logo from "../../LoginAssets/logo.png";
import { Link } from "react-router-dom";
import { FaUserShield } from "react-icons/fa";
import { BsFillShieldLockFill } from "react-icons/bs";
import { AiOutlineSwapRight } from "react-icons/ai";
import { MdOutlineDriveFileRenameOutline } from "react-icons/md";
import { MdOutlinePhoneIphone } from "react-icons/md";

const Register = () => {
  return (
    <div className="registerPage flex">
      <div className="container flex">
        <div className="videoDiv">
          <video src={video} autoPlay muted loop></video>
          <div className="textDiv">
            <h2 className="title">Let us guide you</h2>
            <p>Discover extraordinary places</p>
          </div>
          <div className="footerDiv flex">
            <span className="text">Have an account?</span>
            <Link to={"/"}>
              <button className="btn">Login</button>
            </Link>
          </div>
        </div>

        <div className="formDiv flex">
          <div className="headerDiv">
            <img src={logo} alt="Logo Image" />
            <h3>Let Us Know You!</h3>
          </div>

          <form action="" className="form grid">
            <div className="inputDiv">
              <label htmlFor="firstname">First Name</label>
              <div className="input flex">
                <MdOutlineDriveFileRenameOutline className="icon" />
                <input
                  required
                  type="text"
                  id="firstname"
                  placeholder="Enter First Name"
                />
              </div>
            </div>

            <div className="inputDiv">
              <label htmlFor="lastname">Last Name</label>
              <div className="input flex">
                <MdOutlineDriveFileRenameOutline className="icon" />
                <input
                  required
                  type="text"
                  id="lastname"
                  placeholder="Enter Last Name"
                />
              </div>
            </div>

            <div className="inputDiv">
              <label htmlFor="email">Email</label>
              <div className="input flex">
                <FaUserShield className="icon" />
                <input
                  required
                  type="text"
                  id="email"
                  placeholder="Enter Email"
                />
              </div>
            </div>

            <div className="inputDiv">
              <label htmlFor="phonenumber">Phone Number</label>
              <div className="input flex">
                <MdOutlinePhoneIphone className="icon" />
                <input
                  required
                  type="text"
                  id="phonenumber"
                  placeholder="Enter Phone Number"
                />
              </div>
            </div>

            <div className="inputDiv">
              <label htmlFor="password">Password</label>
              <div className="input flex">
                <BsFillShieldLockFill className="icon" />
                <input
                  required
                  type="password"
                  id="password"
                  placeholder="Enter Password"
                />
              </div>
            </div>

            <button type="submit" className="btn flex">
              <span>Register</span>
              <AiOutlineSwapRight className="icon" />
            </button>
          </form>
        </div>
      </div>
    </div>
  );
};

export default Register;
