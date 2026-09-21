"use client";
import React from 'react'
import {useGetMainCategoriesQuery} from "../../apis/mainCategoryApi";

function Navbar() {
    const {data: categories, error, isLoading} = useGetMainCategoriesQuery();

    console.log("categories:", categories);


  return (
    <div>
        {
            categories.result.map((category) => (
                <div key={category.id}>
                    <h1>{category.categoryName}</h1>
                </div>
            ))
        }
    </div>
  )
}

export default Navbar