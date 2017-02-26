﻿using System;
using UniversalistDergiRC.Model;
using UniversalistDergiRC.ViewModels;
using UniversalistDergiRC.Views;
using Xamarin.Forms;

namespace UniversalistDergiRC
{
    public class NavigationController
    {
        private CarouselPage detailCarouselPage;
        private CarouselPage menuCarouselPage;
        private MasterDetailPage mainPage;
        public NavigationController()
        {

        }
        public void SetPages(MasterDetailPage mainMasterDetail, CarouselPage detailCarousel, CarouselPage menuCarousel)
        {
            menuCarouselPage = menuCarousel;
            detailCarouselPage = detailCarousel;
            mainPage = mainMasterDetail;
        }

        public void OpenReadingPage(int issueNumber, int pageNumber)
        {
            if (detailCarouselPage == null || issueNumber == 0)
                return;

            if (detailCarouselPage.Children.Count == 1)
                detailCarouselPage.Children.Add(new ReadingPageView());

            ReadingPageView readingPage = detailCarouselPage.Children[1] as ReadingPageView;

            if (readingPage == null) return;

            mainPage.IsPresented = false;

            ReadingPageViewModel vmReadingPage = readingPage.BindingContext as ReadingPageViewModel;
            if (vmReadingPage == null) return;
            detailCarouselPage.IsBusy = true;
            vmReadingPage.OpenMagazine(issueNumber, pageNumber);
            detailCarouselPage.IsBusy = false;

            detailCarouselPage.CurrentPage = detailCarouselPage.Children[1];
        }

        internal void OpenBookmarkListPage()
        {
            if (menuCarouselPage == null)
                return;

            if (menuCarouselPage.Children.Count == 1)
                menuCarouselPage.Children.Add(new BookmarkListView(this));

            BookmarkListView bookmarkListPage = menuCarouselPage.Children[1] as BookmarkListView;

            if (bookmarkListPage == null) return;

            BookmarkListViewModel vmBookmarkList = bookmarkListPage.BindingContext as BookmarkListViewModel;
            if (vmBookmarkList == null) return;

            menuCarouselPage.IsBusy = true;
            vmBookmarkList.ListAllBookMarks();
            menuCarouselPage.IsBusy = false;

            menuCarouselPage.CurrentPage = menuCarouselPage.Children[1];
        }

        internal void CloseBookmarkListPage()
        {
            if (menuCarouselPage == null || menuCarouselPage.Children.Count == 0)
                return;
            menuCarouselPage.CurrentPage = menuCarouselPage.Children[0];
        }

        internal void OpenMagazineListPage()
        {
            if (detailCarouselPage == null || detailCarouselPage.Children.Count == 0)
                return;
            mainPage.IsPresented = false;
            detailCarouselPage.CurrentPage = detailCarouselPage.Children[0];
        }
    }
}