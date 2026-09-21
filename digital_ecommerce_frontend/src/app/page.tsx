"use client";
import Image from "next/image";
import {useDispatch, useSelector } from "react-redux";
import {increment, decrement, incrementBy, reset} from "../redux/slices/counterSlice";
import Navbar from "./components/Navbar";

export default function Home() {
  const counter = useSelector((state: any) => state.counter.value);
  const dispatch = useDispatch();

  
  
  return (
    <div className="flex flex-col flex-1 items-center justify-center bg-zinc-50 font-sans dark:bg-black">
      <Navbar></Navbar>
    </div>
  );
}
