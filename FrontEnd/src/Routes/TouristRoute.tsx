import React from "react";
import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "../Context/useAuth";

type Props = { children: React.ReactNode };

const TouristRoute = ({ children }: Props) => {
  const location = useLocation();
  const { user } = useAuth();

  return user?.role === "Tourist" ? (
    <>{children}</>
  ) : (
    <Navigate to="/dashboard" state={{ from: location }} replace />
  );
};

export default TouristRoute;
