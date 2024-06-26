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
import DashboardGuide from "../Components/Guide/DashboardGuide/DashboardGuide";
import MyTripsGuide from "../Components/Guide/MyTripsGuide/MyTripsGuide";
import NotificationsGuide from "../Components/Guide/NotificationsGuide/NotificationsGuide";
import MyAccountGuide from "../Components/Guide/MyAccountGuide/MyAccountGuide";
import ApplyTourist from "../Components/Tourist/ApplyTourist/ApplyTourist";
import TripsParisTourist from "../Components/Tourist/TripsParisTourist/TripsParisTourist";
import TripsLondonTourist from "../Components/Tourist/TripsLondonTourist/TripsLondonTourist";
import TripsRomeTourist from "../Components/Tourist/TripsRomeTourist/TripsRomeTourist";
import TripsBerlinTourist from "../Components/Tourist/TripsBerlinTourist/TripsBerlinTourist";

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
              <DashboardGuide />
            </GuideRoute>
          </ProtectedRoute>
        ),
      },
      {
        path: "guide/my-trips",
        element: (
          <ProtectedRoute>
            <GuideRoute>
              <MyTripsGuide />
            </GuideRoute>
          </ProtectedRoute>
        ),
      },
      {
        path: "guide/notifications",
        element: (
          <ProtectedRoute>
            <GuideRoute>
              <NotificationsGuide />
            </GuideRoute>
          </ProtectedRoute>
        ),
      },
      {
        path: "guide/account",
        element: (
          <ProtectedRoute>
            <GuideRoute>
              <MyAccountGuide />
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
      // {
      //   path: "dashboard",
      //   element: (
      //     <ProtectedRoute>
      //       <TouristRoute>
      //         <Dashboard />
      //       </TouristRoute>
      //     </ProtectedRoute>
      //   ),
      // },
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
      {
        path: "tourist/apply",
        element: (
          <ProtectedRoute>
            <TouristRoute>
              <ApplyTourist />
            </TouristRoute>
          </ProtectedRoute>
        ),
      },
      {
        path: "tourist/trip/paris",
        element: (
          <ProtectedRoute>
            <TouristRoute>
              <TripsParisTourist />
            </TouristRoute>
          </ProtectedRoute>
        ),
      },
      {
        path: "tourist/trip/london",
        element: (
          <ProtectedRoute>
            <TouristRoute>
              <TripsLondonTourist />
            </TouristRoute>
          </ProtectedRoute>
        ),
      },
      {
        path: "tourist/trip/rome",
        element: (
          <ProtectedRoute>
            <TouristRoute>
              <TripsRomeTourist />
            </TouristRoute>
          </ProtectedRoute>
        ),
      },
      {
        path: "tourist/trip/berlin",
        element: (
          <ProtectedRoute>
            <TouristRoute>
              <TripsBerlinTourist />
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
      // {
      //   path: "test",
      //   element: <Test />,
      // },
    ],
  },
]);
