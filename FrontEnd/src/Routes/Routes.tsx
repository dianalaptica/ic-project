import { createBrowserRouter } from "react-router-dom";
import Login from "../Components/Login/Login";
import Register from "../Components/Register/Register";
import Dashboard from "../Components/Dashboard OLD/DashboardOLD";
import App from "../App";
import ProtectedRoute from "./ProtectedRoute";
import Admin from "../Components/Admin/Admin";
import AdminRoute from "./AdminRoute";
import TouristRoute from "./TouristRoute";
import Test from "../Components/Test/Test";
import DashboardTourist from "../Components/Tourist/DashboardTourist/DashboardTourist";
import GuideRoute from "./GuideRoute";
import MyTripsTourist from "../Components/Tourist/MyTripsTourist/MyTripsTourist";
import FindTripsTourist from "../Components/Tourist/FindTripsTourist/FindTripsTourist";
import NotificationsTourist from "../Components/Tourist/NotificationsTourist/NotificationsTourist";
import MyAccountTourist from "../Components/Tourist/MyAccountTourist/MyAccountTourist";

// create a router
export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <Login /> },
      { path: "register", element: <Register /> },
      {
        path: "guide/dashboard",
        element: (
          <ProtectedRoute>
            <GuideRoute>
              <Test />
            </GuideRoute>
          </ProtectedRoute>
        ),
      },
      {
        path: "tourist/dashboard",
        element: (
          <ProtectedRoute>
            <TouristRoute>
              <DashboardTourist />
            </TouristRoute>
          </ProtectedRoute>
        ),
      },
      {
        path: "dashboard",
        element: (
          <ProtectedRoute>
            <TouristRoute>
              <Dashboard />
            </TouristRoute>
          </ProtectedRoute>
        ),
      },
      {
        path: "admin",
        element: (
          <ProtectedRoute>
            <AdminRoute>
              <Admin />
            </AdminRoute>
          </ProtectedRoute>
        ),
      },
      {
        path: "tourist/my-trips",
        element: (
          <ProtectedRoute>
            <TouristRoute>
              <MyTripsTourist />
            </TouristRoute>
          </ProtectedRoute>
        ),
      },
      {
        path: "tourist/find-trips",
        element: (
          <ProtectedRoute>
            <TouristRoute>
              <FindTripsTourist />
            </TouristRoute>
          </ProtectedRoute>
        ),
      },
      {
        path: "tourist/notifications",
        element: (
          <ProtectedRoute>
            <TouristRoute>
              <NotificationsTourist />
            </TouristRoute>
          </ProtectedRoute>
        ),
      },
      {
        path: "tourist/account",
        element: (
          <ProtectedRoute>
            <TouristRoute>
              <MyAccountTourist />
            </TouristRoute>
          </ProtectedRoute>
        ),
      },
    ],
  },
]);
