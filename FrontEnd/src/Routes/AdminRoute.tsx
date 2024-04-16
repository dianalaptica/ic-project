import React from "react";
import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "../Context/useAuth";

type Props = { children: React.ReactNode };

const AdminRoute = ({ children }: Props) => {
  const location = useLocation();
  const { user } = useAuth();

  return user?.role === "Admin" ? (
    <>{children}</>
  ) : (
    <Navigate to="/dashboard" state={{ from: location }} replace />
  );
};

export default AdminRoute;
