import React from "react";
import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "../Context/useAuth";

type Props = { children: React.ReactNode };

const GuideRoute = ({ children }: Props) => {
  const location = useLocation();
  const { user } = useAuth();

  if (user?.role === "Guide") {
    return <>{children}</>;
  } else if (user?.role === "Admin") {
    return <Navigate to="/admin" state={{ from: location }} replace />;
  } else if (user?.role === "Tourist") {
    return (
      <Navigate to="/tourist/dashboard" state={{ from: location }} replace />
    );
  } else {
    // Fallback in case the role doesn't match any of the expected roles
    return <Navigate to="/" state={{ from: location }} replace />;
  }
};

export default GuideRoute;
