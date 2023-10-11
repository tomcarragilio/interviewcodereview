# Interview Test - Simple Cache

Welcome to the interview test repository. This repository contains a simple caching solution intended to improve the performance of a stateless API.

## Overview

The main piece of code in this repository is the `SimpleCache<T>` class within the `StatelessApi` namespace. The idea behind this cache is to store data temporarily, thus potentially reducing the number of requests or lookups to an external data source, and consequently, improving the performance of the system.

## Interfaces

There are two main interfaces, these are included in the same file for convenience of reading:

1. `ILogger`: Provides a basic logging mechanism.
2. `IDataFetcher<T>`: Represents the data fetching strategy based on a provided key.

## Implementation

The `SimpleCache<T>` class uses a dictionary (`_cacheStore`) for storing cache items. Each item has an expiration time. If a key is requested and the data is available in the cache and has not expired, it's returned directly from the cache. Otherwise, the data is fetched using the `IDataFetcher<T>` implementation and then stored in the cache.

## Your Task

Your task, as an interviewee, is to:

1. Review the code.
2. Identify any issues, whether they are related to performance, design, code quality, or any other aspect you deem important.
3. Suggest improvements.

While reviewing, consider the primary use-case for this code which is a cache to improve the performance of a stateless API.
