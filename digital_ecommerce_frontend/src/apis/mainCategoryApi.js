import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { BASE_URL } from "../constanst/url";

export const mainCategoryApi = createApi({
    reducerPath: 'mainCategoryApi',
    baseQuery: fetchBaseQuery({
        baseUrl: BASE_URL,
        prepareHeaders: (headers) => {
            headers.set('Content-Type', 'application/json');
            return headers;
        }
    }),
    tagTypes: ['MainCategory'],
    endpoints: (builder) => ({
        getMainCategories: builder.query({
            query: () => 'MainCategory',
            providesTags: ['MainCategory'],
        })
    })
})

export const {useGetMainCategoriesQuery} = mainCategoryApi;