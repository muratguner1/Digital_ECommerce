import { configureStore, combineReducers } from "@reduxjs/toolkit";
import storage from "redux-persist/lib/storage";
import {
  persistStore,
  persistReducer,
  FLUSH,
  REHYDRATE,
  PAUSE,
  PERSIST,
  PURGE,
  REGISTER,
} from "redux-persist";
import counterReducer from "./slices/counterSlice";
import {mainCategoryApi} from "../apis/mainCategoryApi";

const persistConfig = {
    key: "root",
    storage,
};

const rootReducer = combineReducers({
    counter: counterReducer,
    [mainCategoryApi.reducerPath]: mainCategoryApi.reducer,
});

const persistedReducer = persistReducer(persistConfig, rootReducer);

export const store = configureStore({
    reducer: persistedReducer,
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware({
            serializableCheck: {
                ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER],
            },
        }).concat(mainCategoryApi.middleware),
});

export const persistor = persistStore(store);
