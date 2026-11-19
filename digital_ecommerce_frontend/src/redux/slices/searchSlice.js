import { createSlice } from "@reduxjs/toolkit";

const searchSlice = createSlice({
    name: "search",
    initialState: { 
        value: '',
        categoryId: null,
    },
    reducers: {
        setSearchQuery: (state, action) => {
            state.value = action.payload;
        },
        clearSeacrhQuery: (state) => {
            state.value = '';
        },
        resetSearchQuery: (state) => {
            state.value = '';
        },
        setSearchCategoryId: (state, action) => {
            state.categoryId = action.payload;
        },
        resetSearchCategoryId: (state) => {
            state.categoryId = null;
        }
    }
})

export const {
    setSearchQuery, 
    clearSeacrhQuery, 
    resetSearchQuery,
    setSearchCategoryId,
    resetSearchCategoryId
} = searchSlice.actions
export default searchSlice.reducer