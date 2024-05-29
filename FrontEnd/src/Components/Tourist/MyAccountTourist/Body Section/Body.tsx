import { FaUserShield } from "react-icons/fa";
import "./Body.css";
import Top from "./Top Section/Top";
import {
  MdOutlineDriveFileRenameOutline,
  MdOutlinePhoneIphone,
} from "react-icons/md";
import { AiOutlineSwapRight } from "react-icons/ai";
import { useEffect, useState } from "react";
import { useForm } from "react-hook-form";

type EditDetailsFormInputs = {
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  profilePicture?: Blob;
};

const Body = () => {
  // TO DO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
  // change this mocked data to what api returns
  const mockedData: EditDetailsFormInputs = {
    email: "aaa",
    firstName: "gica",
    lastName: "hagi",
    phoneNumber: "07777",
  };

  const [isLoading, setIsLoading] = useState<boolean>(false);
  const [isChanged, setIsChanged] = useState<boolean>(false);

  const { register, handleSubmit, watch } = useForm<EditDetailsFormInputs>({
    defaultValues: {
      email: mockedData.email,
      firstName: mockedData.firstName,
      lastName: mockedData.lastName,
      phoneNumber: mockedData.phoneNumber,
    },
  });

  const watchedValues = watch();

  useEffect(() => {
    const hasChanged = Object.keys(mockedData).some(
      (key) =>
        watchedValues[key as keyof EditDetailsFormInputs] !==
        mockedData[key as keyof EditDetailsFormInputs]
    );
    setIsChanged(hasChanged);
  }, [watchedValues, mockedData]);

  // TO DO !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
  // creeaza functia editPersonalDetails care face call ul catre api

  const handleEdit = async (form: EditDetailsFormInputs) => {
    setIsLoading(true);
    await editPersonalDetails(
      form.email,
      form.firstName,
      form.lastName,
      form.phoneNumber,
      form.profilePicture
    );
    setIsLoading(false);
  };

  return (
    <div className="mainContent">
      <Top />
      <br />
      <br />
      <br />
      <h3>See and edit your personal details</h3>
      <div className="bottom flex">
        <form
          action=""
          className="form grid"
          onSubmit={handleSubmit(handleEdit)}
        >
          <div className="inputDiv">
            <label htmlFor="email">Email</label>
            <div className="input flex">
              <FaUserShield className="icon" />
              <input
                type="text"
                id="email"
                placeholder="Enter Email"
                {...register("email")}
              />
            </div>
          </div>

          <div className="inputDiv">
            <label htmlFor="firstname">First Name</label>
            <div className="input flex">
              <MdOutlineDriveFileRenameOutline className="icon" />
              <input
                type="text"
                id="firstName"
                placeholder="Enter First Name"
                {...register("firstName")}
              />
            </div>
          </div>

          <div className="inputDiv">
            <label htmlFor="lastname">Last Name</label>
            <div className="input flex">
              <MdOutlineDriveFileRenameOutline className="icon" />
              <input
                type="text"
                id="lastName"
                placeholder="Enter Last Name"
                {...register("lastName")}
              />
            </div>
          </div>

          <div className="inputDiv">
            <label htmlFor="phonenumber">Phone Number</label>
            <div className="input flex">
              <MdOutlinePhoneIphone className="icon" />
              <input
                type="text"
                id="phonenumber"
                placeholder="Enter Phone Number"
                {...register("phoneNumber")}
              />
            </div>
          </div>

          <label htmlFor="phonenumber">Profile picture</label>
          <input
            type="file"
            className="file-input file-input-bordered w-full max-w-xs"
            id="profilePicture"
            {...register("profilePicture")}
          />

          {isLoading === false ? (
            <button
              type="submit"
              className={`btn ${!isChanged ? "disable" : ""} flex`}
              disabled={!isChanged}
            >
              <span>Submit</span>
              <AiOutlineSwapRight className="icon" />
            </button>
          ) : (
            <span className="loading loading-dots loading-lg"></span>
          )}
        </form>
      </div>
    </div>
  );
};

export default Body;
