import React from "react";
import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "../Context/useAuth";

type Props = { children: React.ReactNode };

const TouristRoute = ({ children }: Props) => {
  const location = useLocation();
  const { user } = useAuth();

  if (user?.role === "Tourist") {
    return <>{children}</>;
  } else if (user?.role === "Admin") {
    return <Navigate to="/admin" state={{ from: location }} replace />;
  } else if (user?.role === "Guide") {
    return (
      <Navigate to="/guide/dashboard" state={{ from: location }} replace />
    );
  } else {
    return <Navigate to="/" state={{ from: location }} replace />;
  }
};

export default TouristRoute;
