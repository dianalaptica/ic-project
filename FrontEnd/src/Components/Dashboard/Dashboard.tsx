import React from "react";
import "../../App.css";
import { useAuth } from "../../Context/useAuth";

const Dashboard = () => {
  const { isLoggedIn, user, logout } = useAuth();

  return (
    <>
      <h1>Dashboard</h1>
      {isLoggedIn() ? (
        <div className="container flex">
          <h3>Welcome, {user?.email} </h3>
          <button className="btn flex" onClick={logout}>
            Logout
          </button>
        </div>
      ) : (
        <div></div>
      )}
    </>
  );
};

export default Dashboard;
