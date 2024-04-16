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
import * as Yup from "yup";
import { useAuth } from "../../Context/useAuth";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";

type RegisterFormInputs = {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  password: string;
};

const validation = Yup.object().shape({
  firstName: Yup.string().required("First Name is required"),
  lastName: Yup.string().required("Last Name is required"),
  email: Yup.string().required("Email is required"),
  phoneNumber: Yup.string().required("Phone Number is required"),
  password: Yup.string().required("Password is required"),
});

const Register = () => {
  const { registerUser } = useAuth();
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterFormInputs>({ resolver: yupResolver(validation) });

  const handleRegister = (form: RegisterFormInputs) => {
    registerUser(
      form.firstName,
      form.lastName,
      form.email,
      form.phoneNumber,
      form.password
    );
  };

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

          <form
            action=""
            className="form grid"
            onSubmit={handleSubmit(handleRegister)}
          >
            <div className="inputDiv">
              <label htmlFor="firstname">First Name</label>
              <div className="input flex">
                <MdOutlineDriveFileRenameOutline className="icon" />
                <input
                  type="text"
                  id="firstname"
                  placeholder="Enter First Name"
                  {...register("firstName")}
                />
                {errors.firstName ? <p>{errors.firstName.message}</p> : ""}
              </div>
            </div>

            <div className="inputDiv">
              <label htmlFor="lastname">Last Name</label>
              <div className="input flex">
                <MdOutlineDriveFileRenameOutline className="icon" />
                <input
                  type="text"
                  id="lastname"
                  placeholder="Enter Last Name"
                  {...register("lastName")}
                />
                {errors.lastName ? <p>{errors.lastName.message}</p> : ""}
              </div>
            </div>

            <div className="inputDiv">
              <label htmlFor="email">Email</label>
              <div className="input flex">
                <FaUserShield className="icon" />
                <input
                  type="text"
                  id="email"
                  placeholder="Enter Email"
                  {...register("email")}
                />
                {errors.email ? <p>{errors.email.message}</p> : ""}
              </div>
            </div>

            <div className="inputDiv">
              <label htmlFor="phonenumber">Phone Number</label>
              <div className="input flex">
                <MdOutlinePhoneIphone className="icon" />
                <input
                  type="text"
                  id="phonenumber"
                  placeholder="Enter Phone Number"
                  {...register("phoneNumber")}
                />
                {errors.phoneNumber ? <p>{errors.phoneNumber.message}</p> : ""}
              </div>
            </div>

            <div className="inputDiv">
              <label htmlFor="password">Password</label>
              <div className="input flex">
                <BsFillShieldLockFill className="icon" />
                <input
                  type="password"
                  id="password"
                  placeholder="Enter Password"
                  {...register("password")}
                />
                {errors.password ? <p>{errors.password.message}</p> : ""}
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
