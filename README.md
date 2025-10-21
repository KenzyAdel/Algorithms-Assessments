# Algorithm Assessments Collection

Three algorithmic problems covering divide-and-conquer, greedy algorithms, and dynamic programming.

## Problems

### 1. Sorting Comparison - Quick vs Quick-Insertion Sort

**Algorithm Type:** Divide and Conquer

**Description:**  
Sort an array of N numbers using two algorithms:
1. Standard Quick Sort
2. Quick-Insertion Sort Hybrid (switches to insertion sort below threshold)

**Complexity:** O(N log N)

---

### 2. Task Scheduling II - Preemptive Scheduling

**Algorithm Type:** Greedy

**Description:**  
Schedule N tasks on a single CPU with preemption to minimize average completion time. Each task has:
- Processing time: pᵢ
- Release time: rᵢ

**Complexity:** < O(N²)

---

### 3. School's Quiz VI - Subset Sum Count

**Algorithm Type:** Dynamic Programming

**Description:**  
Given K positive integers and target N, count all combinations that sum to N. Each element used once maximum.

**Complexity:** Polynomial time

---

## Repository Structure

```
algorithms-assessments/
├── 1-Sorting-Comparison/
├── 2-Task-Scheduling-II/
└── 3-School's-Quiz-VI/
```

## Complexity Summary

| Problem | Type | Time Complexity |
|---------|------|-----------------|
| Sorting Comparison | Divide & Conquer | O(N log N) |
| Task Scheduling II| Greedy | < O(N²) |
| School's Quiz VI | Dynamic Programming | Polynomial |

---

## Documentation

Each problem directory contains a detailed PDF specification with complete problem statements, constraints, requirements, and helper functions.