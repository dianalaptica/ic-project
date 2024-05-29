import "./Login.css";
import "../../App.css";
import video from "../../LoginAssets/video.mp4";
import logo from "../../LoginAssets/logo.png";
import { Link } from "react-router-dom";
import { FaUserShield } from "react-icons/fa";
import { BsFillShieldLockFill } from "react-icons/bs";
import { AiOutlineSwapRight } from "react-icons/ai";
import * as Yup from "yup";
import { useAuth } from "../../Context/useAuth";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import { useState } from "react";

type LoginFormInputs = {
  email: string;
  password: string;
};

const validation = Yup.object().shape({
  email: Yup.string().required("Email is required"),
  password: Yup.string().required("Password is required"),
});

const Login = () => {
  const { loginUser } = useAuth();
  const [isLoading, setIsLoading] = useState<boolean>(false);
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormInputs>({ resolver: yupResolver(validation) });

  const handleLogin = async (form: LoginFormInputs) => {
    setIsLoading(true);
    console.log(form.email);
    await loginUser(form.email, form.password);
    setIsLoading(false);
  };

  return (
    <div className="loginPage flex">
      <div className="container flex">
        <div className="videoDiv">
          <video src={video} autoPlay muted loop></video>
          <div className="textDiv">
            <h2 className="title">Let us guide you</h2>
            <p>Discover extraordinary places</p>
          </div>
          <div className="footerDiv flex">
            <span className="text">Don't have an account?</span>
            <Link to={"/register"}>
              <button className="btn">Sign Up</button>
            </Link>
          </div>
        </div>

        <div className="formDiv flex">
          <div className="headerDiv">
            <img src={logo} alt="Logo Image" />
            <h3>Welcome Back!</h3>
          </div>

          <form
            action=""
            className="form grid"
            onSubmit={handleSubmit(handleLogin)}
          >
            {/* <span className="showMessage">Login status</span> */}

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
              <label htmlFor="password">Password</label>
              <div className="input flex">
                <BsFillShieldLockFill className="icon" />
                <input
                  required
                  type="password"
                  id="password"
                  placeholder="Enter Password"
                  {...register("password")}
                />
                {errors.password ? <p>{errors.password.message}</p> : ""}
              </div>
            </div>

            {isLoading === false ? (
              <button type="submit" className="btn flex">
                <span>Login</span>
                <AiOutlineSwapRight className="icon" />
              </button>
            ) : (
              <span className="loading loading-dots loading-lg"></span>
            )}

            {/* <span className="forgotPassword">
              Forgot your password? <a href="">Click Here</a>
            </span> */}
          </form>
        </div>
      </div>
    </div>
  );
};

export default Login;
