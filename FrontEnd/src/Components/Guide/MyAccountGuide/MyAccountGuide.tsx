import { useAuth } from "../../../Context/useAuth";
import Body from "./Body Section/Body";
import SideBar from "./SideBar/SideBar";

const MyAccountGuide = () => {
  const { isLoggedIn } = useAuth();

  return (
    <>
      {isLoggedIn() ? (
        <div className="dashboard flex">
          <div className="dashboardContainer flex">
            <SideBar />
            <Body />
          </div>
        </div>
      ) : (
        <div></div>
      )}
    </>
  );
};

export default MyAccountGuide;
