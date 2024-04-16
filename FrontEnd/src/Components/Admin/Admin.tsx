import React from "react";
import { useAuth } from "../../Context/useAuth";
import "../../App.css";

const Admin = () => {
  const { isLoggedIn, user, logout } = useAuth();

  return (
    <>
      <h1>Admin</h1>
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

export default Admin;
