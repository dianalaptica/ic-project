import "../../App.css";
import { useAuth } from "../../Context/useAuth.tsx";
import useAxiosPrivate from "../../Hooks/useAxiosPrivate.ts";

const Dashboard = () => {
  const { isLoggedIn, user, logout } = useAuth();
  const axiosPrivate = useAxiosPrivate();

  // TODO: This is a test on how it should work
  // FROM NOW USE ONLY AXIOS PRIVATE
  const callApi = async () => {
    try {
      const response = await axiosPrivate.get("/trips");
      console.log(response.data);
    } catch (err) {
      console.log(err);
    }
  };

  console.log(isLoggedIn());

  return (
    <>
      <h1>Dashboard</h1>
      {isLoggedIn() ? (
        <div className="container flex">
          <h3>Welcome, {user?.email} </h3>
          <button className="btn flex" onClick={logout}>
            Logout
          </button>
          <button className="btn flex" onClick={callApi}>
            TEST ME :)
          </button>
        </div>
      ) : (
        <div></div>
      )}
    </>
  );
};

export default Dashboard;
