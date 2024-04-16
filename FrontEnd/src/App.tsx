import { Outlet } from "react-router-dom";
import "./App.css";
import { ToastContainer } from "react-toastify";
import "react-toastify/ReactToastify.css";
import { UserProvider } from "./Context/useAuth";

function App() {
  return (
    <>
      <UserProvider>
        <Outlet />
        <ToastContainer />
      </UserProvider>
    </>
  );
}

export default App;
