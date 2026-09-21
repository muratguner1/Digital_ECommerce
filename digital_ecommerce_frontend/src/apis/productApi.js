import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { BASE_URL } from "../constanst/url";

export const productApi = createApi({
    reducerPath: 'productApi',
    baseQuery: fetchBaseQuery({
        baseUrl: `${BASE_URL}/Product/`,
        prepareHeaders: (headers) => {
            headers.set('Content-Type', 'application/json');
            return headers;
        }
    }),
    tagTypes: ['Product'],
    endpoints: (builder) => ({
        getProductsWithPagination: builder.mutation({
            query: (model) => ({
                url: 'GetWithPagination',
                method: 'POST',
                body: model,
            }),
            invalidatesTags: ['Product'],
        })
    })
})

export const {useGetProductsWithPaginationMutation} = productApi;