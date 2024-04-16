import { createBrowserRouter } from "react-router-dom";
import Login from "../Components/Login/Login";
import Register from "../Components/Register/Register";
import Dashboard from "../Components/Dashboard/Dashboard";
import App from "../App";

// create a router
export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <Login /> },
      { path: "register", element: <Register /> },
      { path: "dashboard", element: <Dashboard /> },
    ],
  },
]);
